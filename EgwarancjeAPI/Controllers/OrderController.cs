using EgwarancjeDbLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EgwarancjeAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly LocalDatabaseContext _database;


    public OrderController(LocalDatabaseContext database)
    {
        _database = database;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult<List<Order>>> GetAllOrders()
    {
        List<Order> orders = await _database.Orders.ToListAsync();
        return Ok(orders);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<ActionResult<Order>> AddOrder(Order order)
    {
        _database.Orders.Add(order);
        await _database.SaveChangesAsync();

        return Ok(order);
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var dbOrder = await _database.Orders.FindAsync(id);
        if (dbOrder is null) return BadRequest($"Order with {id} doesn't exists");

        _database.Orders.Remove(dbOrder);
        await _database.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    [Route("Spec/GetAll")]
    public async Task<ActionResult<List<OrderSpec>>> GetAllOrdersSpecs()
    {
        List<OrderSpec> ordersSpecs = await _database.OrdersSpec.ToListAsync();
        return Ok(ordersSpecs);
    }

    [HttpPost]
    [Route("Spec/Add")]
    public async Task<IActionResult> AddOrderSpec(OrderSpec orderSpec)
    {
        _database.OrdersSpec.Add(orderSpec);
        await _database.SaveChangesAsync();

        return Created();
    }

    [HttpDelete]
    [Route("Spec/Delete")]
    public async Task<IActionResult> DeleteOrderSpec(int id)
    {
        var dbOrderSpec = await _database.OrdersSpec.FindAsync(id);
        if (dbOrderSpec is null) return BadRequest($"Order spec with {id} doesn't exists");

        _database.OrdersSpec.Remove(dbOrderSpec);
        await _database.SaveChangesAsync();

        return Ok();
    }
}
