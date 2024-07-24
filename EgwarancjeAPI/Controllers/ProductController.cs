using EgwarancjeDbLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EgwarancjeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly LocalDatabaseContext _database;

        public ProductController(LocalDatabaseContext database)
        {
            _database = database;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await _database.Products.ToListAsync();

            return Ok(products);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult<List<Product>>> UpdateProducts (Product product)
        {
            var dbProduct = await _database.Products.FindAsync(product.Id);
            if (dbProduct is null) return BadRequest("Product not found.");

            dbProduct.Name = product.Name;
            dbProduct.ImagePath = product.ImagePath;
            dbProduct.Structure = product.Structure;
            await _database.SaveChangesAsync();

            return Ok(await _database.Products.ToListAsync());
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var dbProduct = _database.Products.FirstOrDefault(u => u.Name!.Equals(product.Name));
            if (dbProduct is not null) return BadRequest($"Product with {dbProduct.Name} exists");

            _database.Products.Add(product);
            await _database.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
        {
            var dbProduct = await _database.Products.FindAsync(id);
            if (dbProduct is null) return BadRequest("Product not found.");

            _database.Products.Remove(dbProduct);
            await _database.SaveChangesAsync();

            return Ok(await _database.Products.ToListAsync());
        }
    }
}
