﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopService.Models;
using ShopService.Data;

namespace ShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly RetailStoreDataContext? _context;
        public ProductController(RetailStoreDataContext? context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context!.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context!.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            return product;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product? product)
        {
            if (id != product!.Id)
                return BadRequest();

            _context!.Entry(product).State = EntityState.Modified;

            try
            {
                await _context!.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product? product)
        {
            _context!.Products.Add(product!);
            await _context!.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product!.Id }, product!);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _context!.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context!.Products.Remove(product);
            await _context!.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context!.Products.Any(x => x.Id == id);
        }
    }
}