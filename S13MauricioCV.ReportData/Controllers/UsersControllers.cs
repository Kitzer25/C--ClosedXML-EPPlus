using Application.UseCases.User.Commands;
using Application.UseCases.User.Querys;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace S13MauricioCV.ReportData.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllUsersQuery(), ct));
 
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => Ok(await mediator.Send(new GetUserByIdQuery { UserId = id }, ct));
 
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddNewUserCommand command, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetAll), new { }, result);
    }
 
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand command, CancellationToken ct)
    {
        command.UserId = id;
        return Ok(await mediator.Send(command, ct));
    }
 
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        => Ok(await mediator.Send(new DeleteUserCommand { UserId = id }, ct));
}