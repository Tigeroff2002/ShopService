using Auth0.AspNetCore.Authentication;
using Data.Contexts;
using Data.Repositories;
using Data.Repositories.Abstractions;
using Logic;
using Logic.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using ShopService.Views.Account;

namespace ShopService.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : Controller
{
    public OrderController(
        ILogger<OrderController> logger,
        IClientsRepository clientsRepository,
        IBasketsRepository basketsRepository,
        IOrdersRepository ordersRepository,
        IOrderManager orderManager)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ordersRepository = ordersRepository ?? throw new ArgumentNullException(nameof(ordersRepository));
        _basketsRepository = basketsRepository ?? throw new ArgumentNullException(nameof(basketsRepository));
        _clientsRepository = clientsRepository ?? throw new ArgumentNullException(nameof(clientsRepository));

        _orderManager = orderManager ?? throw new ArgumentNullException(nameof(orderManager));

        _logger.LogInformation("Order Controller was started just now");
    }

    [HttpGet("{userId:int}/createOrder")]
    public async Task<IActionResult> CreateOrder(int userId)
    {
        if (ModelState.IsValid)
        {
            var user = await _clientsRepository.FindAsync(userId, CancellationToken.None)
                .ConfigureAwait(false);

            if (user == null)
            {
                return RedirectToAction("Login", "Account", new LoginModel());
            }

            if (user!.Basket == null)
            {
                user!.Basket = new Basket(user);
                user!.Basket!.SummUpProducts = new List<SummUpProduct>();
            }

            var findedBasket = _basketsRepository.FindLastBasket(userId, CancellationToken.None);

            if (findedBasket == null)
            {
                findedBasket = user!.Basket;
            }

            var order = new Order(user);

            if (findedBasket!.SummUpProducts == null)
            {
                order.SummUpProducts = new List<SummUpProduct>();
            }

            order.SummUpProducts = findedBasket!.SummUpProducts!.ToList();

            if (order.SummUpProducts.Count == 0)
            {
                order = SeedTestOrder(user);
            }

            order.ResultCost = order.CalculateResultCost();

            if (order.ResultCost == 0)
            {
                return RedirectToAction("BasketPage", (findedBasket, user));
            }

            order.OrderDate = DateTime.Now;

            order.ShippedDate = DateTime.Today.AddDays(1);

            var task = Task.Run(async () => await _orderManager.ProcessOrdersAsync(CancellationToken.None)
                .ConfigureAwait(false));

            task.Wait();

            var processedOrder = await _orderManager.CreateAsync(order, CancellationToken.None)
                .ConfigureAwait(false);

            processedOrder.isReadyForConfirmation = true;

            await _ordersRepository.AddOrderAsync(processedOrder, CancellationToken.None);

            _ordersRepository.SaveChanges();

            return View("OrderHome", (processedOrder, user));
        }

        return RedirectToAction("AuthIndex", "Home", new {id = userId});
    }

    [HttpPut("{userId:int}/changeOrder/{id:int}")]
    public IActionResult ChangeOrder(int userId, int id)
    {
        var order = _ordersRepository.Find(id, CancellationToken.None);

        if (order == null)
        {
            return NotFound();
        }

        _ordersRepository.SaveChanges();

        var testUser = new User
        {
            Id = 1,
            Role = new Role(0)
        };

        return Ok((order, testUser));
    }

    public async Task<IActionResult> CancelOrder(int userId)
    {
        var user = await _clientsRepository.FindAsync(userId, CancellationToken.None)
            .ConfigureAwait(false);

        if (user == null)
        {
            return RedirectToAction("Login", "Account", new LoginModel());
        }

        if (user!.Basket == null)
        {
            user!.Basket = new Basket(user);
            user!.Basket!.SummUpProducts = new List<SummUpProduct>();
        }

        var findedBasket = _basketsRepository.FindLastBasket(userId, CancellationToken.None);

        if (findedBasket == null)
        {
            findedBasket = user!.Basket;
        }

        var order = new Order(user);

        if (findedBasket!.SummUpProducts == null)
        {
            order.SummUpProducts = new List<SummUpProduct>();
        }

        order.SummUpProducts = findedBasket!.SummUpProducts!.ToList();

        if (order.SummUpProducts.Count == 0)
        {
            order = SeedTestOrder(user);
        }

        order.ResultCost = order.CalculateResultCost();

        order.OrderDate = DateTime.Now;

        order.ShippedDate = DateTime.Today.AddDays(1);

        order.isDeleted = true;

        _orderManager.CancelOrder(order, CancellationToken.None);

        _ordersRepository.SaveChanges();

        return RedirectToAction("GetBasket", "Basket", new {userId = userId});
    }

    [HttpGet("{userId:int}/confirmOrder/{id:int}")]
    public async Task<IActionResult> ConfirmOrder(int userId, int id)
    {
        if (ModelState.IsValid)
        {
            var user = await _clientsRepository.FindAsync(userId, CancellationToken.None)
                            .ConfigureAwait(false);

            if (user == null)
            {
                return RedirectToAction("Login", "Account", new LoginModel());
            }

            if (user!.Basket == null)
            {
                user!.Basket = new Basket(user);
                user!.Basket!.SummUpProducts = new List<SummUpProduct>();
            }

            var findedOrder = _ordersRepository.FindOrder(userId, CancellationToken.None);

            if (findedOrder == null)
            {
                findedOrder = new Order(user);
            }

            var order = new Order(user);

            if (findedOrder!.SummUpProducts == null)
            {
                order.SummUpProducts = new List<SummUpProduct>();
            }

            order.SummUpProducts = findedOrder!.SummUpProducts!.ToList();

            if (order.SummUpProducts.Count == 0)
            {
                order = SeedTestOrder(user);
            }

            order.ResultCost = order.CalculateResultCost();

            _ordersRepository.SaveChanges();

            order.ResultCost = order.CalculateResultCost();

            var confirmedOrder = await _orderManager.ConfirmOrderAsync(order, CancellationToken.None)
                .ConfigureAwait(false);

            confirmedOrder.isReadyForConfirmation = true;
            confirmedOrder.isReadyForPayment = true;
            confirmedOrder.isPayd = true;

            _ordersRepository.UpdateOrder(confirmedOrder, CancellationToken.None);

            _ordersRepository.SaveChanges();

             return View("OrderConfirm", (confirmedOrder, user));
        }

        return RedirectToAction("AuthIndex", "Home", new {id = userId});
    }

    [HttpGet("{userId:int}/takeOrder/{id:int}")]
    public async Task<IActionResult> TakeOrder(int userId, int id)
    {
        if (ModelState.IsValid)
        {
            var user = await _clientsRepository.FindAsync(userId, CancellationToken.None)
                .ConfigureAwait(false);

            if (user == null)
            {
                return RedirectToAction("Login", "Account", new LoginModel());
            }

            var order = new Order(user);

            var takenOrder = await _orderManager.GiveOrderAsync(order, CancellationToken.None)
                .ConfigureAwait(false);

            _ordersRepository.UpdateOrder(takenOrder, CancellationToken.None);

            return RedirectToAction("AuthIndex", "Home", new { id = userId });
        }

        return RedirectToAction("AuthIndex", "Home", new { id = userId });
    }


    [HttpGet("{userId:int}/getOrder/{id:int}")]
    public IActionResult GetOrder(int userId, int id)
    {
        var order = _ordersRepository.Find(id, CancellationToken.None);

        if (order == null)
        {
            return NotFound();
        }

        if (order.Client == null)
        {
            return BadRequest();
        }

        var user = order.Client;

        order = SeedTestOrder(user);

        return View((order, user));
    }

    [HttpGet("{adminId:int}/cancelUserOrder/{id:int}")]
    public async Task<IActionResult> CancelUserOrder(int adminId, int id)
    {
        var order = _ordersRepository.FindOrder(id, CancellationToken.None);

        var admin = await _clientsRepository.FindAsync(adminId, CancellationToken.None)
            .ConfigureAwait(false);

        if (order == null)
        {
            if (admin != null)
            {
                return RedirectToAction("AdminPageShow", "Home", new { id = admin!.Id });
            }
        }

        var user = await _clientsRepository.FindAsync(order!.Id, CancellationToken.None)
            .ConfigureAwait(false);

        order.isDeleted = true;

        order.OrderDescription = "Deleted By Admin";

        //_orderManager.CancelOrder(order, CancellationToken.None);

        _ordersRepository.DeleteOrder(order, CancellationToken.None);

        var index = user!.OldOrders!.FindIndex(x => x.Id == id);

        user!.OldOrders!.RemoveAt(index);

        _clientsRepository.Update(user, CancellationToken.None);

        _ordersRepository.SaveChanges();

        return RedirectToAction("AdminPageShow", "Home", new { id = admin!.Id });
    }

    [HttpGet("ordersList/{userId:int}")]
    public async Task<ActionResult<IEnumerable<Order>>> GetUserOrdersAsync(int userId, bool skipLast)
    {
        if (ModelState.IsValid)
        {
            var user = await _clientsRepository.FindAsync(userId, CancellationToken.None)
                .ConfigureAwait(false);

            if (user == null)
            {
                return NotFound();
            }

            return _ordersRepository.GetAllUserOrders(user, CancellationToken.None, skipLast);
        }

        return RedirectToAction("Index", "Home");
    }

    private Order SeedTestOrder(User user)
    {
        var order = new Order
        {
            Client = user,
            OrderDate = DateTime.Now,
            ShippedDate = DateTime.Now - TimeSpan.FromDays(1),
            SummUpProducts = new List<SummUpProduct>
                {
                    new SummUpProduct
                    {
                        Product = new Product
                        {
                            Name = "Iphone 13",
                            DeviceType = new DeviceType
                            {
                                Name = "Смартфон"
                            },
                            Producer = new Producer
                            {
                                Name = "Apple"
                            },
                            Cost = 60_000
                        },
                        Quantity = 3,
                        TotalPrice = 180_000
                    }
                },
        };

        return order;
    }

    private readonly ILogger<OrderController> _logger;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IBasketsRepository _basketsRepository;
    private readonly IClientsRepository _clientsRepository;
    private readonly IOrderManager _orderManager;
}
