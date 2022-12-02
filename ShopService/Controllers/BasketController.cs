using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Models;
using Data.Contexts;
using System.Security.Cryptography;

namespace ShopService.Controllers
{
    [Route("api/basket")]
    [ApiController]
    public class BasketController : Controller
    {
        private readonly ILogger<BasketController> _logger;
        private readonly RetailStoreDataContext? _context;
        private int ClientId { get; set; } 
        public BasketController(ILogger<BasketController> logger, RetailStoreDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("getBasket")]
        public async Task<ActionResult<Basket>> GetBasket()
        {
            /*
            var user = await _context!.Clients.FindAsync(clientId);
            if (user == null)
            {
                user = new User(1, 1);
                var basket = user.Basket = new Basket(1, user);
                return View(basket);
            }
            var existedBasket = user!.Basket!;
            if (existedBasket == null)
                return NotFound();

             */
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

            var user = new User
            {
                Id = 1,
                Role = new Role(0)
            };

            return View("BasketPage", (existedBasket, user));
        }

        [HttpGet("cleanBasket")]
        public async Task<ActionResult> CleanBasket()
        {
            //_context!.Clients.First(x => x.Id == clientId).Basket!.SummUpProducts!.Clear();
            //await _context!.SaveChangesAsync();

            var user = new User
            {
                Id = 1,
                Role = new Role(0)
            };

            var basket = new Basket
            {
                BasketStatusId = 1,
                Client = user
            };

            return View("BasketPage", (basket, user));
        }
        
        private int FindQuantityProductsBasket(int id)
        {
            if (_context!.Clients!.First(x => x.Id == ClientId).Basket!.SummUpProducts!.Any(x => x.Product!.Id == id))
                return _context!.Clients!.First(x => x.Id == ClientId).Basket!.SummUpProducts!.First(x => x.Product!.Id == id).Quantity;
            else
                return 0;
        }

        public async Task<ActionResult<Basket>> AddProductToBasket(int id, int count)
        {
            var group = await TryAddProductToBasket(id, count);

            var user = new User
            {
                Id = 1,
                Role = new Role(0)
            };

            var basket = new Basket
            {
                BasketStatusId = 1,
                Client = user
            };

            if (group == null)
            {
                return View("BasketPage", (basket, user));
            }

            basket = new Basket
            {
                BasketStatusId = 1,
                SummUpProducts = new HashSet<SummUpProduct>(),
                TotalCost = group.TotalPrice
            };

            basket.SummUpProducts.Add(group);

            return View("BasketPage", (basket, user));
        }

        private async Task<SummUpProduct> TryAddProductToBasket(int id, int count)
        {
            var product = await _context!.Products.FindAsync(id);
            if (product == null)
                return null!;
            var summUpProduct = new SummUpProduct
            {
                Id = 1,
                Product = product,
                Quantity = count,
                TotalPrice = product.Cost * count
            };
            return summUpProduct;

        }

        [HttpPost("removeProduct/{id}")]
        public async Task<ActionResult<Basket>> RemoveProductFromBasket(int id)
        {
            var product = await _context!.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            int count = FindQuantityProductsBasket(id);
            var summUpProductId = 1;
            if (count < 2)
                if (count == 0)
                    return BadRequest();
                else if (count == 1)
                {
                    _context!.Clients.First(x => x.Id == ClientId).Basket!.SummUpProducts!
                                     .Remove(new SummUpProduct(summUpProductId, product, 1));
                }
            else
            {
                _context!.Clients.Where(x => x.Id == ClientId)
                             .ToList()
                             .Select(x => { x.Basket!.SummUpProducts!.First(x => x.Product!.Id == id).Quantity = --count; return x; });
            }
            _context!.Entry(product).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(ClientId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetProductItem", new { id = product!.Id }, product);
        }

        private bool ClientExists(int id)
        {
            return _context!.Clients!.Any(x => x.Id == id);
        }

        private bool SummUpProductExists(int id)
        {
            return _context!.Clients!.First(x => x.Id == 1).Basket!.SummUpProducts!.Any(x => x.Product!.Id == id);
        }

    }
}
