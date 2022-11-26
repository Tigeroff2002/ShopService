using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Models;
using Data.Contexts;

namespace ShopService.Controllers
{
    [Route("api/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;
        private readonly RetailStoreDataContext? _context;
        private int ClientId { get; set; } 
        public BasketController(ILogger<BasketController> logger, RetailStoreDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<Basket>> GetBasket(int clientId)
        {
            var user = await _context!.Clients.FindAsync(clientId);
            var basket = user!.Basket!;
            if (basket == null)
                return NotFound();

            return basket;
        }

        [HttpDelete]
        public async Task<ActionResult> CleanBasket(int clientId)
        {
            _context!.Clients.First(x => x.Id == clientId).Basket!.SummUpProducts!.Clear();
            await _context!.SaveChangesAsync();
            return NoContent();
        }
        
        private int FindQuantityProductsBasket(int id)
        {
            if (_context!.Clients!.First(x => x.Id == ClientId).Basket!.SummUpProducts!.Any(x => x.Product!.Id == id))
                return _context!.Clients!.First(x => x.Id == ClientId).Basket!.SummUpProducts!.First(x => x.Product!.Id == id).Quantity;
            else
                return 0;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Basket>> AddProductToBasket(int id)
        {
            var product = await _context!.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            int count = FindQuantityProductsBasket(id);
            if (count == 0)
                _context!.Clients.First(x => x.Id == ClientId).Basket!.SummUpProducts!
                                 .Add(new SummUpProduct 
                                 {
                                     Product = product, 
                                     Quantity = 1, 
                                     TotalPrice = product!.Cost 
                                 });
            else
            {
                _context!.Clients.Where(x => x.Id == ClientId)
                             .ToList()
                             .Select(x => { x.Basket!.SummUpProducts!.First(x => x.Product!.Id == id).Quantity = ++count; return x; });
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

        [HttpPost("{id}")]
        public async Task<ActionResult<Basket>> RemoveProductFromBasket(int id)
        {
            var product = await _context!.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            int count = FindQuantityProductsBasket(id);
            if (count < 2)
                if (count == 0)
                    return BadRequest();
                else if (count == 1)
                {
                    _context!.Clients.First(x => x.Id == ClientId).Basket!.SummUpProducts!
                                     .Remove(new SummUpProduct
                                     {
                                        Product = product,
                                        Quantity = 1,
                                        TotalPrice = product!.Cost
                                     });
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
