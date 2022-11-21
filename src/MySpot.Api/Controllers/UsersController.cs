using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ICommandHandler<SignUp> _signUpHandler;
    public UsersController(ICommandHandler<SignUp> signUpHandler)
    {
        _signUpHandler = signUpHandler;
    }

    [HttpPost]
    public async Task<ActionResult> Post(SignUp command)
    {
        await _signUpHandler.HandleAsync(command with { UserId = Guid.NewGuid() });
        return NoContent();
    }
}