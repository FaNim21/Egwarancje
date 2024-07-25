using EgwarancjeDbLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EgwarancjeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly LocalDatabaseContext _database;


        public CartController(LocalDatabaseContext database)
        {
            _database = database;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<Cart>>> GetAllCart()
        {
            var carts = await _database.Carts.ToListAsync();

            return Ok(carts);
        }


    }
}
