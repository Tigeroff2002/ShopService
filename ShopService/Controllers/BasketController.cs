﻿using Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models;
using ShopService.Views.Account;
using System.Transactions;

namespace ShopService.Controllers;

[Route("api/basket")]
[ApiController]
public class BasketController : Controller
{
    public BasketController(
        ILogger<BasketController> logger,
        IProductsRepository productsRepository,
        ISummUpProductsRepository summUpProductsRepository,
        IBasketsRepository basketsRepository,
        IClientsRepository clientsRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
        _summUpProductsRepository = summUpProductsRepository ?? throw new ArgumentNullException(nameof(summUpProductsRepository));
        _basketsRepository = basketsRepository ?? throw new ArgumentNullException(nameof(basketsRepository));
        _clientsRepository = clientsRepository ?? throw new ArgumentNullException(nameof(clientsRepository));

        _logger.LogInformation("Basket Controller was started just now");
    }

    [HttpGet("getBasket/{userId:int}")]
    public async Task<IActionResult> GetBasket(int userId)
    {
        var user = await _clientsRepository.FindAsync(userId, CancellationToken.None)
            .ConfigureAwait(false);

        if (user == null)
        {
            return RedirectToAction("Login", "Account", new LoginModel());
        }

        var findedBasket = _basketsRepository.FindLastBasket(userId, CancellationToken.None);

        if (findedBasket == null)
        {
            findedBasket = new Basket(user);

            await _basketsRepository.AddBasketAsync(findedBasket, CancellationToken.None)
                .ConfigureAwait(false);

            _basketsRepository.SaveChanges();
        }

        if (user!.Basket == null)
        {
            user!.Basket = findedBasket;
        }

        return View("BasketPage", (findedBasket, user));
    }

    [HttpGet("cleanBasket/{userId:int}")]
    public async Task<IActionResult> CleanBasket(int userId)
    {
        var user = await _clientsRepository.FindAsync(userId, CancellationToken.None)
            .ConfigureAwait(false);

        if (user == null)
        {
            return RedirectToAction("Login", "Account", new LoginModel());
        }

        var findedBasket = _basketsRepository.FindLastBasket(userId, CancellationToken.None);

        if (findedBasket == null)
        {
            findedBasket = new Basket(user);
        }

        findedBasket.SummUpProducts = new List<SummUpProduct>();

        user!.Basket = findedBasket;

        await _basketsRepository.AddBasketAsync(findedBasket, CancellationToken.None)
            .ConfigureAwait(false);

        return View("BasketPage", (findedBasket, user));
    }

    private int FindQuantityProductsBasket(int id)
    {
        /*
        if (_context!.Clients!
            .First(x => x.Id == _clientId)
            .Basket!.SummUpProducts!
            .Any(x => x.Product!.Id == id))
        {
            return _context!.Clients!
                .First(x => x.Id == _clientId).Basket!
                .SummUpProducts!
                .First(x => x.Product!.Id == id)
                .Quantity;
        }
        */

        return 0;
    }

    [HttpGet("addProductBasket/{userId:int}")]
    public async Task<ActionResult<Basket>> AddProductToBasket(int userId, int id, int count)
    {
        var group = await TryAddProductToBasket(id, count);

        var user = await _clientsRepository.FindAsync(userId, CancellationToken.None)
            .ConfigureAwait(false);

        if (user == null)
        {
            return RedirectToAction("Login", "Account", new LoginModel());
        }

        var findedBasket = _basketsRepository.FindLastBasket(userId, CancellationToken.None);

        if (findedBasket == null)
        {
            findedBasket = new Basket(user);
        }

        var newBasket = new Basket(user);

        foreach (var item in findedBasket.SummUpProducts)
        {
            newBasket.SummUpProducts.Add(item);
        }

        foreach (var groupItem in findedBasket.SummUpProducts)
        {
            if (groupItem.ProductId == group.ProductId)
            {
                newBasket.SummUpProducts.Remove(groupItem);

                group.Quantity += groupItem.Quantity;

                newBasket.SummUpProducts.Add(group);

                group = null!;

                break;
            }
        }

        if (group != null)
        {
            newBasket.SummUpProducts.Add(group);
        }

        foreach (var item in newBasket.SummUpProducts)
        {
            findedBasket.SummUpProducts.Add(item);
        }

        foreach (var itemGroup in findedBasket.SummUpProducts)
        {
            if (itemGroup.Quantity == 0)
            {
                newBasket.SummUpProducts.Remove(itemGroup);
            }
        }

        newBasket.TotalCost = newBasket.CalculateTotalPrice();

        var newUser = new User
        {
            UserId = user.UserId,
            NickName = user!.NickName,
            EmailAdress = user!.EmailAdress,
            Password = user!.Password,
            ContactNumber = user!.Password,
            Role = user!.Role,
            Basket = new Basket(user)
        };

        newUser!.Basket.SummUpProducts = new List<SummUpProduct>();

        foreach (var item in newBasket.SummUpProducts)
        {
            newUser!.Basket!.SummUpProducts.Add(item);
        }

        _clientsRepository.Delete(user, CancellationToken.None);

        await _clientsRepository.AddAsync(newUser, CancellationToken.None)
            .ConfigureAwait(false);

        await _basketsRepository.AddBasketAsync(newBasket, CancellationToken.None)
            .ConfigureAwait(false);

        await _summUpProductsRepository.Add(group!, CancellationToken.None);

        _clientsRepository.SaveChanges();

        return RedirectToAction("AuthIndex", "Home", new {id = user!.UserId});
    }

    private async Task<SummUpProduct> TryAddProductToBasket(int id, int count)
    {
        var product = await _productsRepository.FindProduct(id, CancellationToken.None)
            .ConfigureAwait(false);

        if (product == null)
        {
            return null!;
        }

        var summUpProduct = new SummUpProduct
        {
            Product = product,
            Quantity = count,
            TotalPrice = product.Cost * count
        };

        return summUpProduct;
    }

    [HttpPost("removeProduct/{id}")]
    public async Task<ActionResult<Basket>> RemoveProductFromBasket(int id)
    {
        var product = await _productsRepository.FindProduct(id, CancellationToken.None)
            .ConfigureAwait(false);

        if (product == null)
        {
            return NotFound();
        }

        int count = FindQuantityProductsBasket(id);

        var summUpProductId = 1;

        if (count < 2)
        {
            if (count == 0)
            {
                return BadRequest();
            }
            else
                if (count == 1)
            {
                /*
                _context!.Clients
                    .First(x => x.Id == _clientId)
                    .Basket!
                    .SummUpProducts!
                    .Remove(
                        new SummUpProduct(
                            summUpProductId,
                            product, 
                            1));
                */
            }
        }
        else
        {
            /*
            _context!.Clients
                .Where(x => x.Id == _clientId)
                .ToList()
                .Select(x => 
                    { 
                        x.Basket!.SummUpProducts!
                            .First(x => x.Product!.Id == id)
                            .Quantity = --count;
                        return x; 
                    });
            */
        }

        //_context!.Entry(product).State = EntityState.Modified;

        _basketsRepository.SaveChanges();

        return CreatedAtAction("GetProductItem",
            new
            {
                id = product!.Id
            },
            product);
    }

    private async Task<bool> ClientExists(int userId)
    {
        return await _clientsRepository.FindAsync(userId, CancellationToken.None)
            != null;
    }

    private bool SummUpProductExists(int id)
    {
        /*
        return _context!.Clients!
            .First(x => x.Id == 1)
            .Basket!
            .SummUpProducts!
            .Any(x => x.Product!.Id == id);
        */

        return true;
    }

    private Basket SeedExistedBasket()
    {
#pragma warning disable CS8670 // Инициализатор объекта или коллекции неявно разыменовывает член, который может быть равен NULL.
        var existedBasket = new Basket
        {
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
#pragma warning restore CS8670 // Инициализатор объекта или коллекции неявно разыменовывает член, который может быть равен NULL.

        return existedBasket;
    }

    private readonly ILogger<BasketController> _logger;
    private readonly IProductsRepository _productsRepository;
    private readonly ISummUpProductsRepository _summUpProductsRepository;
    private readonly IBasketsRepository _basketsRepository;
    private readonly IClientsRepository _clientsRepository;
}
