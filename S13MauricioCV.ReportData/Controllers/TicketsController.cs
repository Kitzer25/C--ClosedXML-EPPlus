using Application.UseCases.Ticket.Commands;
using Application.UseCases.Ticket.Querys;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace S13MauricioCV.ReportData.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllTicketsQuery(), ct));
 
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => Ok(await mediator.Send(new GetTicketByIdQuery { TicketId = id }, ct));
 
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUser(Guid userId, CancellationToken ct)
        => Ok(await mediator.Send(new GetTicketsByUserQuery { UserId = userId }, ct));
 
    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(string status, CancellationToken ct)
        => Ok(await mediator.Send(new GetTicketsByStatusQuery { Status = status }, ct));
 
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddTicketCommand command, CancellationToken ct)
    {
        Console.WriteLine(command.Status);
        var result = await mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetAll), new { }, result);
    }
 
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTicketCommand command, CancellationToken ct)
    {
        command.TicketId = id;
        return Ok(await mediator.Send(command, ct));
    }
 
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        => Ok(await mediator.Send(new DeleteTicketCommand { TicketId = id }, ct));
}