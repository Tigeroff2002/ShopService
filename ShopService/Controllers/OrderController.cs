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

        [HttpPost("create/{id:int}")]
        public IActionResult Create(Order order)
        {
            ArgumentNullException.ThrowIfNull(_context);

            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        [HttpPut("confirm/{id:int}")]
        public IActionResult Confirm(Order order)
        {
            ArgumentNullException.ThrowIfNull(_context);

            if (ModelState.IsValid)
            {
                _context!.Entry(order).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return NoContent();
        }


        [HttpGet("check/{id:int}")]
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
