using Application.UseCases.Response.Querys;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace S13MauricioCV.ReportData.Controllers;

[ApiController]
[Route("api/tickets")]
public class ResponsesController(ISender mediator) : ControllerBase
{
    [HttpGet("{ticketId:guid}/responses")]
    public async Task<IActionResult> GetResponses(Guid ticketId, CancellationToken ct)
    {
        var query = new GetResponsesByTicketQuery { TicketId = ticketId };
        
        var result = await mediator.Send(query, ct);
        
        if (!result.Any())
        {
            return NotFound("No se encontraron respuestas para este ticket.");
        }

        return Ok(result);
    }
}