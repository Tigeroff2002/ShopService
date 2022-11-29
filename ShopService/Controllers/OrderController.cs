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
            return View("OrderHome", new Order());
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

        [HttpDelete("cancelOrder/{id:int}")]
        public async Task<ActionResult> CancelOrder(int id)
        {
            ArgumentNullException.ThrowIfNull(_context);

            var order = await _context!.Orders.FindAsync(id);
            if (order == null)
                return NotFound();

            _context!.Orders.Remove(order);

            await _context!.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("confirmOrder/{id:int}")]
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
