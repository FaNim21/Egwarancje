using EgwarancjeAPI.Services.Messages;
using EgwarancjeDbLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EgwarancjeAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly LocalDatabaseContext _database;
    private readonly IMessagesService _messagesService;

    public AccountController(IMessagesService messagesService, LocalDatabaseContext database)
    {
        _messagesService = messagesService; 
        _database = database;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> RegisterUser(UserDto userDto)
    {
        var dbUser = _database.Users.FirstOrDefault(u => u.Email.Equals(userDto.Email));
        if (dbUser is not null) return BadRequest($"User with {dbUser.Email} exists");

        string confirmationToken = Guid.NewGuid().ToString();
        string? confirmationLink = Url.Action("SendActivationLink", "Account", new { token = confirmationToken, email = userDto.Email }, Request.Scheme);
        bool success = _messagesService.SendAccountConfirmationEmail(userDto.Email, userDto.Name, confirmationLink!);
        if(!success) return BadRequest(new { Message = "Could not send the email"});

        User user = new()
        {
            Name = userDto.Name,
            Email = userDto.Email,
            IsActivated = false,
            Password = userDto.Password,
            PhoneNumber = userDto.PhoneNumber,
            ConfirmationToken = confirmationToken
        };
        _database.Users.Add(user);
        await _database.SaveChangesAsync();

        return Ok(new { Message = "Registration successful. Please check your email to confirm your account." });
    }
    
    [HttpGet]
    [Route("SendActivationLink")]
    public async Task<IActionResult> SendActivationLink(string token, string email)
    {
        var user = _database.Users.FirstOrDefault(u => u.Email.Equals(email));
        if (user == null || (string.IsNullOrEmpty(user.ConfirmationToken) && user.ConfirmationToken != token))
        {
            return BadRequest(new { Message = "Invalid token or email." });
        }

        user.IsActivated = true;
        user.ConfirmationToken = null;

        await _database.SaveChangesAsync(); 

        return Ok(new { Message = "Email confirmed successfully." });
    }

    [HttpPost]
    [Route("ActivateWithoutToken")]
    public async Task<IActionResult> ActivateProfile(string to)
    {
        var user = await _database.Users.FirstOrDefaultAsync(u => u.Email.Equals(to));
        if (user == null) return BadRequest("User not found");
        user.IsActivated = true;

        _database.SaveChanges();

        return Ok($"Account '{to}' has been activated");
    }

    [HttpPost]
    [Route("ResetPassword")]
    public async Task<IActionResult> ResetPassword()
    {
        string tempPassword = Guid.NewGuid().ToString("N");
        return Ok(tempPassword);
    }
}


