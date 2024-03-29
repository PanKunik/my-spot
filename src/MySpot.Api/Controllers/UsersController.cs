using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Application.Queries;
using MySpot.Application.Secutiry;
using Swashbuckle.AspNetCore.Annotations;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ICommandHandler<SignUp> _signUpHandler;
    private readonly IQueryHandler<GetUser, UserDTO> _getUserHandler;
    private readonly IQueryHandler<GetUsers, IEnumerable<UserDTO>> _getUsersHandler;
    private readonly ICommandHandler<SignIn> _signInHandler;
    private readonly ITokenStorage _tokenStorage;

    public UsersController(
        ICommandHandler<SignUp> signUpHandler,
        IQueryHandler<GetUser, UserDTO> getUserHandler,
        IQueryHandler<GetUsers, IEnumerable<UserDTO>> getUsersHandler,
        ICommandHandler<SignIn> signInHandler, ITokenStorage tokenStorage)
    {
        _signUpHandler = signUpHandler;
        _getUserHandler = getUserHandler;
        _getUsersHandler = getUsersHandler;
        _signInHandler = signInHandler;
        _tokenStorage = tokenStorage;
    }

    [HttpGet("{userId:guid}")]
    [Authorize(Policy = "is-admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDTO>> Get(Guid userId)
    {
        var user = await _getUserHandler.HandleAsync(new GetUser { UserId = userId });
        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserDTO>> Get()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var isInUserRole = User.IsInRole("user");
        var isInAdminRole = User.IsInRole("admin");

        var userId = Guid.Parse(User.Identity?.Name);
        return await _getUserHandler.HandleAsync(new GetUser { UserId = userId });
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    [SwaggerOperation("Get list of all the users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<UserDTO>>> Get([FromQuery] GetUsers query)
        => Ok(await _getUsersHandler.HandleAsync(query));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(SignUp command)
    {
        command = command with { UserId = Guid.NewGuid() };
        await _signUpHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get), new { command.UserId }, null);
    }

    [HttpPost("sign-in")]
    [SwaggerOperation("Sign in the user and return the Json Web Token")]
    public async Task<ActionResult> Post(SignIn command)
    {
        await _signInHandler.HandleAsync(command);
        return Ok(_tokenStorage.Get());
    }
}