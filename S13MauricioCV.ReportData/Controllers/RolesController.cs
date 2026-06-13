using Application.UseCases.Role.Commands;
using Application.UseCases.Role.Querys;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace S13MauricioCV.ReportData.Controllers;

[ApiController]
[Route("api/roles")]
public class RolesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllRolesQuery(), ct));
 
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => Ok(await mediator.Send(new GetRoleByIdQuery { RoleId = id }, ct));
 
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddNewRoleCommand command, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetAll), new { }, result);
    }
 
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoleCommand command, CancellationToken ct)
    {
        command.RoleId = id;
        return Ok(await mediator.Send(command, ct));
    }
 
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        => Ok(await mediator.Send(new DeleteRoleCommand { RoleId = id }, ct));
}