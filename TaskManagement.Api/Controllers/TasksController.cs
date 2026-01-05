using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Tasks.Commands;
using Microsoft.AspNetCore.Authorization; 

namespace TaskManagement.Api.Controllers;

[Authorize] 
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.StartsWith("Error"))
            return BadRequest(result);

        return Ok(result);
    }
}