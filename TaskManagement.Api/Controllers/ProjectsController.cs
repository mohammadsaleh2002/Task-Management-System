using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Projects.Commands;
using TaskManagement.Application.Projects.Queries;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
	private readonly IMediator _mediator;

	public ProjectsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<IActionResult> Create(CreateProjectCommand command)
	{
		var result = await _mediator.Send(command);
		return Ok(result);
	}

	[HttpPost("add-member")]
	public async Task<IActionResult> AddMember(AddMemberToProjectCommand command)
	{
		var result = await _mediator.Send(command);
		if (result.Contains("not found") || result.Contains("already a member"))
			return BadRequest(result);

		return Ok(result);
	}


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllProjectsQuery()));
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _mediator.Send(new DeleteProjectCommand(id));
        return result ? NoContent() : NotFound();
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateProjectRequest request)
    {
        var command = new UpdateProjectCommand(id, request.Name, request.Description);

        var result = await _mediator.Send(command);

        if (!result) return NotFound("Cannot Find Project");

        return Ok("Yeah We Update Project !");
    }

    public record UpdateProjectRequest(string Name, string? Description);

}