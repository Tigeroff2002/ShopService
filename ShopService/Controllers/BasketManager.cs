using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ShopService.Models;
using ShopService.Data;

namespace ShopService.Controllers
{
    public class BasketManager : ControllerBase
    {
        private readonly ILogger<BasketManager> _logger;
        private readonly RetailStoreDataContext? _context;
        private int ClientId { get; set; } = GetClientId();
        public BasketManager(ILogger<BasketManager> logger, RetailStoreDataContext context)
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
        [HttpGet]
        private async Task<ActionResult<int>> GetClientId()
        {
            var clientId = await _context!.Clients.FindAsync();
            return clientId!.Id;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProductToBasket(Product? product)
        {
            var productItem = new SummUpProduct { Product = product, Quantity = 1, TotalPrice = product!.Cost };
            _context!.Clients.FindAsync().
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProductItem", new { id = productItem.Id }, productItem);
        }

        [HttpPut("{id, count}")]
        public async Task<ActionResult> ChangeQuantityProductsInBasket(int id, SummUpProduct summUpProduct, int count)
        {
            if (id != summUpProduct.Id)
            {
                return BadRequest();
            }

            _context!.Clients.Where(x => x.Id == ClientId)
                             .ToList()
                             .Select(x =>
                             {
                                 x!.Basket!.SummUpProducts!.Where(x => x.Id == id)
                                                           await 
                                                            new SummUpProduct
                                                           {
                                                               Product = summUpProduct.Product,
                                                               Quantity = count,
                                                           };
                                 return x;
                             });
            _context!.Entry(summUpProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }
        }

        private bool SummUpProductExists(int id)
        {
            return _context!.Clients!.First(x => x.Id == 1).Basket!.SummUpProducts!.Any(x => x.Id == id);
        }
    }
}
