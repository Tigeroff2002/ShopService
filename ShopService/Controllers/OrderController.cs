using Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ShopService.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : Controller
    {
        private readonly ILogger<BasketController> _logger;
        private readonly RetailStoreDataContext? _context;

        public OrderController(ILogger<BasketController> logger, RetailStoreDataContext? context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("createOrder")]
        public async Task<ActionResult> CreateOrder()
        {
            ArgumentNullException.ThrowIfNull(_context);
            /*
               if (ModelState.IsValid)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
             */
            return View("OrderHome",
                new Order 
                {
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
                });
        }

        [HttpPut("changeOrder/{id:int}")]
        public async Task<IActionResult> ChangeOrder(int id, Order? order)
        {
            ArgumentNullException.ThrowIfNull(_context);

            if (id != order!.Id)
                return BadRequest();

            _context.Attach(order);

            _context!.Entry(order).State = EntityState.Modified;
            await _context!.SaveChangesAsync();

            return Ok(order);
        }

        [HttpGet("cancelOrder")]
        public async Task<ActionResult> CancelOrder()
        {
            ArgumentNullException.ThrowIfNull(_context);

            /*
            var order = await _context!.Orders.FindAsync(id);
            if (order == null)
                return NotFound();

            _context!.Orders.Remove(order);

            await _context!.SaveChangesAsync();

            return NoContent();
             */
            return RedirectToAction("GetBasket", "Basket");
        }

        [HttpGet("confirmOrder")]
        public IActionResult ConfirmOrder()
        {
            ArgumentNullException.ThrowIfNull(_context);

            /*
            if (ModelState.IsValid)
            {
                _context!.Entry(order).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return NoContent();
             */
            return View("OrderConfirm");
        }


        [HttpGet("getOrder/{id:int}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context!.Orders.FindAsync(id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        [HttpGet("check/listorders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetUserOrdersAsync()
        {
            ArgumentNullException.ThrowIfNull(_context);

            if (ModelState.IsValid)
            {
                return await _context!.Orders!.ToListAsync();
            }
            return NoContent();
        }
    }
}
