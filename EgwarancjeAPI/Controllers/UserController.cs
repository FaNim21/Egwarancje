using EgwarancjeAPI.Services.Messages;
using EgwarancjeDbLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EgwarancjeAPI.Controllers;

/// <summary>
/// Lepiej zamiast bezposrednio user zrobic dto dla user
/// ktory bedzie mial tylko najwazniejsz dane do stworzenia uzytkownika
/// ale na ten moment nie jest to az tak wazne
/// i tak samo zeby uzyc repository pattern
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMessagesService _messagesService;
    private readonly LocalDatabaseContext _database;


    public UserController(IMessagesService messagesService, LocalDatabaseContext database)
    {
        _database = database;
        _messagesService = messagesService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        var users = await _database.Users.ToListAsync();

        return Ok(users);
    }

    [HttpGet]
    [Route("GetByID")]
    public async Task<ActionResult<User>> GetByID(int id)
    {
        var user = await _database.Users.FindAsync(id);
        if (user is null) return BadRequest("User not found.");

        return Ok(user);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<ActionResult<User>> GetUser(string email, string password)
    {
        User? user = await _database.Users
            .Include(u => u.Orders!).ThenInclude(o => o.OrderSpecs)
            .Include(a => a.Addresses)
            .FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(password));

        if (user is null) return BadRequest("User not found.");

        List<Warranty> warranties = await _database.Warranties
                                .Where(w => w.Order.UserId == user!.Id)
                                .Include(w => w.WarrantySpecs)
                                .ThenInclude(w => w.Attachments).ToListAsync();

        user.Warranties = warranties;

        return Ok(user);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> AddUser(User user)
    {
        var dbUser = _database.Users.FirstOrDefault(u => u.Email.Equals(user.Email));
        if (dbUser is not null) return BadRequest($"User with {dbUser.Email} exists");

        _database.Users.Add(user);
        await _database.SaveChangesAsync();

        return Ok();
    }

    [HttpPut]
    [Route("Update")]
    public async Task<ActionResult<List<User>>> UpdateUser(User user)
    {
        var dbUser = await _database.Users.FindAsync(user.Id);
        if (dbUser is null) return BadRequest("User not found.");

        dbUser.Name = user.Name;
        dbUser.Email = user.Email;
        dbUser.Password = user.Password;
        dbUser.PhoneNumber = user.PhoneNumber;

        await _database.SaveChangesAsync();

        return Ok(await _database.Users.ToListAsync());
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<ActionResult<List<User>>> DeleteUser(int id)
    {
        var dbUser = await _database.Users.FindAsync(id);
        if (dbUser is null) return BadRequest("User not found.");

        _database.Users.Remove(dbUser);
        await _database.SaveChangesAsync();

        return Ok(await _database.Users.ToListAsync());
    }
}
