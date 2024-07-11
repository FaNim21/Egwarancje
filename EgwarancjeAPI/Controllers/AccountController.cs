using EgwarancjeAPI.Services.Messages;
using EgwarancjeDbLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EgwarancjeAPI.Controllers;

public struct PostEmail
{
    public string Email { get; set; }
}

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
        if (dbUser is not null) return BadRequest(new { Success = false, Message= $"Isnieje użytkownik o mailu: {dbUser.Email}" });

        string confirmationToken = Guid.NewGuid().ToString();
        string? confirmationLink = Url.Action("SendActivationLink", "Account", new { token = confirmationToken, email = userDto.Email }, Request.Scheme);
        bool success = _messagesService.SendAccountConfirmationEmail(userDto.Email, userDto.Name, confirmationLink!);
        if (!success) return BadRequest(new { Success = false, Message = "Nie udało się wysłać maila" });

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

        return Ok(new { Success = true, Message = "Zarejestrowano pomyślnie. Sprawdź skrzynkę email żeby aktywować konto" });
    }
    
    [HttpGet]
    [Route("SendActivationLink")]
    public async Task<IActionResult> SendActivationLink(string token, string email)
    {
        var user = _database.Users.FirstOrDefault(u => u.Email.Equals(email));
        if (user == null || (string.IsNullOrEmpty(user.ConfirmationToken) && user.ConfirmationToken != token))
        {
            return BadRequest(new { Success = false, Message = "Nie własciwy token lub email" });
        }

        user.IsActivated = true;
        user.ConfirmationToken = null;

        await _database.SaveChangesAsync(); 

        return Ok(new { Success = true, Message = "Konto aktywowano pomyślnie" });
    }

    [HttpPost]
    [Route("ResetPassword")]
    public async Task<IActionResult> ResetPassword(PostEmail postEmail)
    {
        var user = await _database.Users.FirstOrDefaultAsync(u => u.Email.Equals(postEmail.Email));
        if (user is null) return BadRequest(new { Success = false, Message= $"Nie istnieje użytkownik o podanym mailu" });

        string tempPassword = Guid.NewGuid().ToString("N");

        bool success = _messagesService.SendNewPassword(postEmail.Email, user.Name, tempPassword);
        if (!success) return BadRequest(new { Success = false, Message = "Nie udało się wysłać maila" });

        user.Password = tempPassword;
        await _database.SaveChangesAsync();

        return Ok(new { Success = true, Message = $"Wysłano na podanego maila '{postEmail.Email}' nowe hasło" });
    }
}


