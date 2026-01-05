using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Users.Commands;


namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return Ok(new { UserId = userId, Message = "User created successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.StartsWith("Error"))
        {
            return Unauthorized(result);
        }

        return Ok(new { Token = result });
    }
}