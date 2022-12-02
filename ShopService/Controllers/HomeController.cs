using Microsoft.AspNetCore.Mvc;
using ShopService.Models;
using System.Diagnostics;
using Models;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;

namespace ShopService.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RetailStoreDataContext _context;
        private IList<Product>? Devices { get; set; }

        public HomeController(ILogger<HomeController> logger, RetailStoreDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("devicetype/{deviceTypeId:int}")]
        public IActionResult GetProductsByDeviceType(int deviceTypeId)
        {
            Devices = _context.Products.Where(x => x.DeviceType!.Id == deviceTypeId)
                                       .ToList();

            return View(Devices);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = (new List<Product>(), new User());
            try
            {
                model.Item1 = _context.Products.ToList();
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                model.Item1 = new List<Product>();
            }
            catch (InvalidOperationException)
            {
                model.Item1 = new List<Product>();
            }
            if (model.Item2 == null || model.Item2.Role == null)
            {
                model.Item2 = new User 
                {
                    Id = 1,
                    Role = new Role(0) 
                };
            }
            return View(
                "Index", model);
        }

        [HttpGet("Details/{id:int}")]
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest("Некорректный ресурс!");
            Product? device = _context!.Products.FirstOrDefault(x => x.Id == id);
            if (device is null)
                return NotFound("Устройство не найдено!");

            var user = new User
            {
                Id = 1,
                Role = new Role(0)
            };

            return View("Details", (device, user));
        }

        [HttpGet("adminPage")]
        public IActionResult AdminPageShow()
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

            users.First(x => x.Id == 1).Orders.First(x => x.Id == 1).ResultCost = 100_000;
            users.First(x => x.Id == 1).Orders.First(x => x.Id == 1).OrderDescription =
                users.First(x => x.Id == 1).Orders.First(x => x.Id == 1).CreateDescription();

            users.First(x => x.Id == 2).Orders.First(x => x.Id == 1).ResultCost = 50_000;
            users.First(x => x.Id == 2).Orders.First(x => x.Id == 1).OrderDescription =
                users.First(x => x.Id == 2).Orders.First(x => x.Id == 1).CreateDescription();

            users.First(x => x.Id == 2).Orders.First(x => x.Id == 2).ResultCost = 50_000;
            users.First(x => x.Id == 2).Orders.First(x => x.Id == 2).OrderDescription =
                users.First(x => x.Id == 2).Orders.First(x => x.Id == 2).CreateDescription();

            return View("AdminPage", (users, users.First()));
        }

        [HttpGet("managerPage")]
        public IActionResult ManagerPageShow()
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

            var user = new User
            {
                Id = 1,
                Role = new Role(0)
            };

            return View("ManagerPage", (houses, user));
        }

        [HttpPost("{id:int}")]
        public IActionResult Create([Bind(include: "DeviceTypeId, Name, ProducerId")] Product device)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(device);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(device);
        }

        [HttpPut("{id:int}")]
        public IActionResult Edit(Product device)
        {
            return View(device); 
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int? Id)
        {
            Product? device = _context!.Products.FirstOrDefault(x => x.Id == Id);
            return View(device);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}