using EgwarancjeDbLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EgwarancjeAPI.Controllers;

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

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> AddCart(Cart cart)
    {
        var dbCart = _database.Carts.FirstOrDefault(u => u.UserId.Equals(cart.UserId));
        if (dbCart is not null) return BadRequest($"Cart with userid: {dbCart.UserId} exists");

        _database.Carts.Add(cart);
        await _database.SaveChangesAsync();

        return Ok();
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateCart(Cart cart)
    {
        var dbCart = await _database.Carts.FindAsync(cart.Id);
        if (dbCart is null) return BadRequest("Cart not found.");

        dbCart.Products = cart.Products;
        await _database.SaveChangesAsync();
        return Ok(dbCart);
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<ActionResult<List<Cart>>> DeleteCart(int id)
    {
        var dbCart = await _database.Carts.FindAsync(id);
        if (dbCart is null) return BadRequest("Cart not found.");

        _database.Carts.Remove(dbCart);
        await _database.SaveChangesAsync();

        return Ok(await _database.Carts.ToListAsync());
    }
}
