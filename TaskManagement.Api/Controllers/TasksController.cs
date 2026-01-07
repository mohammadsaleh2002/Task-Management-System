using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Tasks.Commands;
using Microsoft.AspNetCore.Authorization;
using TaskManagement.Application.Tasks.Queries;
using TaskManagement.Domain.Enums;

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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllTasksQuery()));
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    => Ok(await _mediator.Send(new GetTaskByIdQuery(id)));

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(string id, [FromBody] TaskItemStatus status)
        => await _mediator.Send(new UpdateTaskStatusCommand(id, status)) ? Ok() : NotFound();

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
        => await _mediator.Send(new DeleteTaskCommand(id)) ? NoContent() : NotFound();

}