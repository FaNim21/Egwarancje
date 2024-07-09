using EgwarancjeAPI.Services.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EgwarancjeAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMessagesService _messagesService;

    public AccountController(IMessagesService messagesService)
    {
        _messagesService = messagesService; 
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> SendActivationLink(string to)
    {
        string? confirmationLink = Url.Action("ConfirmEmail", "Account", new { token = "generatedToken", email = to }, Request.Scheme);

        // Send confirmation email
        //await _messagesService.SendAccountConfirmationEmail(to, subject, confirmationLink);
        string subject = "Account Activation";
        string body = "tu bedzie jakis html z guzikiem do aktywacji";

        _messagesService.Email(subject, body, to);

        return Ok();
    }

    [HttpPost]
    [Route("ResetPassword")]
    public async Task<IActionResult> ResetPassword()
    {
        return Ok();
    }
}


