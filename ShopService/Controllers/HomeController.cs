using Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models;
using ShopService.Models;
using System.Diagnostics;

namespace ShopService.Controllers;

[Route("api/home")]
[ApiController]
public class HomeController : Controller
{
    public HomeController(
        ILogger<HomeController> logger,
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

        _logger.LogInformation("Home Controller was started just now");
    }

    [HttpGet("{id:int}/devicetype/{deviceTypeId:int}")]
    public async Task<IActionResult> GetProductsByDeviceType(int id, int deviceTypeId)
    {
        var model = (
            new List<Product>(),
            new User());

        model.Item1 = await _productsRepository.GetProductsByDeviceType(deviceTypeId, CancellationToken.None)
            .ConfigureAwait(false);

        var user = await _clientsRepository
            .FindAsync(id, CancellationToken.None)
            .ConfigureAwait(false);

        if (model.Item1.Count == 0)
        {
            throw new ArgumentException();
        }

        if (user == null)
        {
            user = new User
            {
                UserId = -1,
                Role = new Role(RoleType.NonAuthUser)
            };
        }

        model.Item2 = user;

        return View("FilterBy", model);
    }

    [HttpGet("{id:int}/producer/{producerId:int}")]
    public async Task<IActionResult> GetProductsByProducer(int id, int producerId)
    {
        var model = (
            new List<Product>(),
            new User());

        model.Item1 = await _productsRepository.GetProductsByProducer(producerId, CancellationToken.None)
            .ConfigureAwait(false);

        var user = await _clientsRepository
            .FindAsync(id, CancellationToken.None)
            .ConfigureAwait(false);

        if (model.Item1.Count == 0)
        {
            throw new ArgumentException();
        }

        if (user == null)
        {
            user = new User
            {
                UserId = -1,
                Role = new Role(RoleType.NonAuthUser)
            };
        }

        model.Item2 = user;

        return View("FilterBy", model);
    }

    [HttpGet("{id:int}/existed")]
    public async Task<IActionResult> GetProductsOnExistense(int id)
    {
        var model = (
            new List<Product>(),
            new User());

        model.Item1 = await _productsRepository.GetProductsOnExistense(CancellationToken.None)
            .ConfigureAwait(false);

        var user = await _clientsRepository
            .FindAsync(id, CancellationToken.None)
            .ConfigureAwait(false);

        if (model.Item1.Count == 0)
        {
            throw new ArgumentException();
        }

        if (user == null)
        {
            user = new User
            {
                UserId = -1,
                Role = new Role(RoleType.NonAuthUser)
            };
        }

        model.Item2 = user;

        return View("FilterBy", model);
    }

    [HttpGet("{id:int}/rating/{rating:int}")]
    public async Task<IActionResult> GetProductsByMark(int id, int rating)
    {
        var model = (
            new List<Product>(),
            new User());

        model.Item1 = await _productsRepository.GetProductsWithMarkAbove(rating, CancellationToken.None)
            .ConfigureAwait(false);

        var user = await _clientsRepository
            .FindAsync(id, CancellationToken.None)
            .ConfigureAwait(false);

        if (model.Item1.Count == 0)
        {
            throw new ArgumentException();
        }

        if (user == null)
        {
            user = new User
            {
                UserId = -1,
                Role = new Role(RoleType.NonAuthUser)
            };
        }

        model.Item2 = user;

        return View("FilterBy", model);
    }

    [HttpGet("{id:int}/cost/{cost:int}")]
    public async Task<IActionResult> GetProductsByCost(int id, int cost)
    {
        var model = (
            new List<Product>(),
            new User());

        model.Item1 = await _productsRepository.GetProductsByDeviceType(cost, CancellationToken.None)
            .ConfigureAwait(false);

        var user = await _clientsRepository
            .FindAsync(id, CancellationToken.None)
            .ConfigureAwait(false);

        if (model.Item1.Count == 0)
        {
            throw new ArgumentException();
        }

        if (user == null)
        {
            user = new User
            {
                UserId = -1,
                Role = new Role(RoleType.NonAuthUser)
            };
        }

        model.Item2 = user;

        return View("FilterBy", model);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = (
            new List<Product>(),
            new User(),
            new Product());

        model.Item1 = await _productsRepository.GetAllProducts(CancellationToken.None);

        if (model.Item1.Count == 0)
            throw new ArgumentException();


        if (model.Item2.Basket == null)
        {
            model.Item2.Basket = new Basket(model.Item2);
        }

        if (model.Item2.Basket.SummUpProducts == null)
        {
            model.Item2.Basket.SummUpProducts = new List<SummUpProduct>();
        }

        model.Item2.UserId = -1;
        model.Item2.Role = new Role(RoleType.NonAuthUser);

        model.Item3.DeviceType = new DeviceType();
        model.Item3.Producer = new Producer();

        return View("Index", model);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> AuthIndex(int id)
    {
        var model = (
            new List<Product>(),
            new User(),
            new Product());

        model.Item1 = await _productsRepository
            .GetAllProducts(CancellationToken.None)
            .ConfigureAwait(false);

        model.Item2 = await _clientsRepository
            .FindAsync(id, CancellationToken.None)
            .ConfigureAwait(false);

        if (model.Item1.Count == 0)
        {
            throw new ArgumentException();
        }

        if (model.Item2 == null)
        {
            throw new ArgumentNullException();
        }

        if (model.Item2.Basket == null)
        {
            model.Item2.Basket = new Basket(model.Item2);
        }

        if (model.Item2.Basket.SummUpProducts == null)
        {
            model.Item2.Basket.SummUpProducts = new List<SummUpProduct>();
        }
        else
        {
            if (model.Item2.Basket.SummUpProducts.Count == 0 && model.Item2.Basket.TotalCost > 0)
            {
                return Content("Ok");
            }
        }

        model.Item3.DeviceType = new DeviceType();
        model.Item3.Producer = new Producer();

        return View("Index", model);
    }

    [HttpGet("{userId:int}/Details/{id:int}")]
    public async Task<IActionResult> Details(int userId, int? id)
    {
        if (id == null)
        {
            return BadRequest("Некорректный ресурс!");
        }

        int _id = id.Value;

        var device = await _productsRepository.FindProduct(_id, CancellationToken.None)
            .ConfigureAwait(false);

        if (device == null)
        {
            return NotFound("Устройство не найдено!");
        }

        var user = await _clientsRepository.FindAsync(userId, CancellationToken.None)
            .ConfigureAwait(false);

        if (user == null)
        {
            return View("Details", (device, new User { Id = -1, Role = new Role(RoleType.NonAuthUser)}));
        }

        return View("Details", (device, user));
    }

    [HttpGet("adminPage/{id:int}")]
    public async Task<IActionResult> AdminPageShow(int id)
    {
        var user = await _clientsRepository.FindAsync(id, CancellationToken.None)
            .ConfigureAwait(false);

        var users = await _clientsRepository.GetAllUsers(CancellationToken.None)
            .ConfigureAwait(false);

        return View("AdminPage", (users.ToList(), user));
    }

    [HttpGet("managerPage/{id:int}")]
    public async Task<IActionResult> ManagerPageShow(int id)
    {
        var houses = SeedWarehousesProducts();

        var user = await _clientsRepository.FindAsync(id, CancellationToken.None)
            .ConfigureAwait(false);

        return View("ManagerPage", (houses, user));
    }

    [HttpPost("{id:int}")]
    public async Task<IActionResult> Create([Bind(include: "DeviceTypeId, Name, ProducerId")] Product device)
    {
        if (ModelState.IsValid)
        {
            await _productsRepository.Add(device, CancellationToken.None)
                .ConfigureAwait(false);

            _productsRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(device);
    }

    [HttpPut("{id:int}")]
    public IActionResult Edit(Product device)
    {
        if (ModelState.IsValid)
        {
            _productsRepository.Update(device, CancellationToken.None);

            _productsRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(device);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return BadRequest("Некорректный ресурс!");
        }

        _productsRepository.Delete(id.Value, CancellationToken.None);

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ??
                    HttpContext.TraceIdentifier
            });
    }

    private List<User> SeedUsersOrders()
    {
        var users = new List<User>
        {
            new User
            {
                Id = 1,
                Orders = new List<Order>
                {
                    new Order
                    {
                        Id = 1,
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
                    }
                }
            },
            new User
            {
                Id = 2,
                Orders = new List<Order>
                {
                    new Order
                    {
                        Id = 1,
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
                        },
                    },
                    new Order
                    {
                        Id = 2,
                        OrderDate = DateTime.Now,
                        ShippedDate = DateTime.Now - TimeSpan.FromDays(1),
                        SummUpProducts = new List<SummUpProduct>
                        {
                            new SummUpProduct
                            {
                                Id = 2,
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
                                },
                                Quantity = 2
                            },
                            new SummUpProduct
                            {
                                Id = 2,
                                Product = new Product
                                {
                                    Name = "Iphone 11 Mini",
                                    DeviceType = new DeviceType
                                    {
                                        Name = "Смартфон"
                                    },
                                    Producer = new Producer
                                    {
                                        Name = "Apple"
                                    }
                                },
                                Quantity = 2
                            }
                        }
                    }
                }
            }
        };

        users
            .First(x => x.Id == 1).Orders
            .First(x => x.Id == 1).ResultCost = 100_000;

        users
            .First(x => x.Id == 1).Orders
            .First(x => x.Id == 1).OrderDescription = users
                    .First(x => x.Id == 1).Orders
                    .First(x => x.Id == 1)
                    .CreateDescription();

        users
            .First(x => x.Id == 2).Orders
            .First(x => x.Id == 1).ResultCost = 50_000;

        users
            .First(x => x.Id == 2).Orders
            .First(x => x.Id == 1).OrderDescription = users
                .First(x => x.Id == 2).Orders
                .First(x => x.Id == 1)
                .CreateDescription();

        users
            .First(x => x.Id == 2).Orders
            .First(x => x.Id == 2).ResultCost = 50_000;

        users
            .First(x => x.Id == 2).Orders
            .First(x => x.Id == 2).OrderDescription = users
                .First(x => x.Id == 2).Orders
                .First(x => x.Id == 2)
                .CreateDescription();

        return users;
    }

    private List<Warehouse> SeedWarehousesProducts()
    {
        var houses = new List<Warehouse>
        {
            new Warehouse
            {
                Id = 1,
                Name = "Склад 1",
                Address = "Корпус 1 ВЛГУ",
                ProductQuantities = new List<SummUpProduct>
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
            },
            new Warehouse
            {
                Id = 2,
                Name = "Склад 2",
                Address = "Корпус 2 ВЛГУ",
                ProductQuantities = new List<SummUpProduct>
                {
                    new SummUpProduct
                    {
                        Id = 1,
                        Product = new Product
                        {
                            Name = "Iphone 11",
                            DeviceType = new DeviceType
                            {
                                Name = "Смартфон"
                            },
                            Producer = new Producer
                            {
                                Name = "AppleRussia"
                            },
                        },
                        Quantity = 2
                    },
                    new SummUpProduct
                    {
                        Id = 2,
                        Product = new Product
                        {
                            Name = "Iphone 5S",
                            DeviceType = new DeviceType
                            {
                                Name = "Смартфон"
                            },
                            Producer = new Producer
                            {
                                Name = "AppleRussia"
                            }
                        },
                        Quantity = 3
                    }
                }
            },
            new Warehouse
            {
                Id = 3,
                Name = "Склад 3",
                Address = "Корпус 3 ВЛГУ",
                ProductQuantities = new List<SummUpProduct>
                {
                    new SummUpProduct
                    {
                        Id = 1,
                        Product = new Product
                        {
                            Name = "Iphone X",
                            DeviceType = new DeviceType
                            {
                                Name = "Смартфон"
                            },
                            Producer = new Producer
                            {
                                Name = "Apple"
                            },
                        },
                        Quantity = 5
                    },
                    new SummUpProduct
                    {
                        Id = 2,
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
                            }
                        },
                        Quantity = 4
                    }
                }
            }
        };

        return houses;
    }

    private readonly ILogger<HomeController> _logger;
    private readonly IProductsRepository _productsRepository;
    private readonly ISummUpProductsRepository _summUpProductsRepository;
    private readonly IBasketsRepository _basketsRepository;
    private readonly IClientsRepository _clientsRepository;
}