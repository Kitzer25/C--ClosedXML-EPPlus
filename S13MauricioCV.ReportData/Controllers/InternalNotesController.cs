using Application.UseCases.InternalNote.Commands;
using Application.UseCases.InternalNote.Querys;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace S13MauricioCV.ReportData.Controllers;

[ApiController]
[Route("api/internal-notes")]
public class InternalNotesController(IMediator mediator) : ControllerBase
{
    [HttpGet("ticket/{ticketId:guid}")]
    public async Task<IActionResult> GetByTicket(Guid ticketId, CancellationToken ct)
        => Ok(await mediator.Send(new GetInternalNotesByTicketQuery { TicketId = ticketId }, ct));
 
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => Ok(await mediator.Send(new GetInternalNoteByIdQuery { NoteId = id }, ct));
 
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddInternalNoteCommand command, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetByTicket), new { ticketId = command.TicketId }, result);
    }
 
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateInternalNoteCommand command, CancellationToken ct)
    {
        command.NoteId = id;
        return Ok(await mediator.Send(command, ct));
    }
 
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        => Ok(await mediator.Send(new DeleteInternalNoteCommand { NoteId = id }, ct));
}