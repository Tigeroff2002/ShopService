using Data.Contexts;
using Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ShopService.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : Controller
{
    public OrderController(
        ILogger<OrderController> logger,
        IClientsRepository clientsRepository,
        IOrdersRepository ordersRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ordersRepository = ordersRepository ?? throw new ArgumentNullException(nameof(ordersRepository));
        _clientsRepository = clientsRepository ?? throw new ArgumentNullException(nameof(clientsRepository));

        _logger.LogInformation("Order Controller was started just now");
    }

    [HttpGet("createOrder/{userId:int}")]
    public async Task<IActionResult> CreateOrder(int userId)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                Id = 1,
                Role = new Role(0)
            };

            var order = SeedTestOrder(user);

            await _ordersRepository.AddOrderAsync(order, CancellationToken.None)
                .ConfigureAwait(false);

            _ordersRepository.SaveChanges();

            return View("OrderHome", (order, user));
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpPut("changeOrder/{id:int}")]
    public IActionResult ChangeOrder(int id)
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

    [HttpPost("cancelOrder/{id:int}")]
    public IActionResult CancelOrder(int id)
    {
        var order = _ordersRepository.Find(id, CancellationToken.None);

        if (order == null)
        {
            return NotFound();
        }

        _ordersRepository.CancelOrder(order, CancellationToken.None);

        _ordersRepository.SaveChanges();

        return RedirectToAction("GetBasket", "Basket");
    }

    [HttpGet("confirmOrder/{id:int}")]
    public IActionResult ConfirmOrder(int id)
    {
        if (ModelState.IsValid)
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

            _ordersRepository.ConfirmOrder(user, order, CancellationToken.None);

             return View("OrderConfirm", (order, user));
        }

        return RedirectToAction("Index", "Home");
    }


    [HttpGet("getOrder/{id:int}")]
    public IActionResult GetOrder(int id)
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

        return View((order, user));
    }

    [HttpGet("ordersList/{userId:int}")]
    public async Task<ActionResult<IEnumerable<Order>>> GetUserOrdersAsync(int userId)
    {
        if (ModelState.IsValid)
        {
            var user = await _clientsRepository.FindAsync(userId, CancellationToken.None)
                .ConfigureAwait(false);

            if (user == null)
            {
                return NotFound();
            }

            return _ordersRepository.GetAllUserOrders(user, CancellationToken.None);
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
                        Id = 1,
                        Product = new Product
                        {
                            Name = "Iphone 14",
                            DeviceType = new DeviceType
                            {
                                Name = "Смартфон"
                            },
                            Producer = new Producer
                            {
                                Name = "Apple"
                            },
                        },
                        Quantity = 2
                    },
                    new SummUpProduct
                    {
                        Id = 2,
                        Product = new Product
                        {
                            Name = "Iphone 12 Mini",
                            DeviceType = new DeviceType
                            {
                                Name = "Смартфон"
                            },
                            Producer = new Producer
                            {
                                Name = "Apple"
                            }
                        },
                        Quantity = 1
                    }
                }
        };

        return order;
    }

    private readonly ILogger<OrderController> _logger;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IClientsRepository _clientsRepository;
}
