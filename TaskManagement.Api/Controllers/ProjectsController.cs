using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Projects.Commands;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
	private readonly ISender _sender;

	public ProjectsController(ISender sender)
	{
		_sender = sender;
	}

	[HttpPost]
	public async Task<IActionResult> Create(CreateProjectCommand command)
	{
		var result = await _sender.Send(command);
		return Ok(result);
	}
}