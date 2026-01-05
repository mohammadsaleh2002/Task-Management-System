using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Projects.Commands;

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
}