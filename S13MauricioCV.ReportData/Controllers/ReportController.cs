using Application.UseCases.Report.Querys;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace S13MauricioCV.ReportData.Controllers;

[ApiController]
public class ReportController : ControllerBase
{
    private readonly IMediator _mediator;
    private const string ExcelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    public ReportController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("reporte")]
    public async Task<IActionResult> DownloadTicketsReport(CancellationToken ct)
    {
        var fileBytes = await _mediator.Send(new GetTicketsExcelQuery(), ct);
        string fileName = $"Reporte_Tickets_{DateTime.Now:yyyyMMdd}.xlsx";
    
        return File(fileBytes, ExcelContentType, fileName);
    }
    
    [HttpGet("categorias-solicitadas")]
    public async Task<IActionResult> DownloadTopCategoriesReport(CancellationToken ct)
    {
        var fileBytes = await _mediator.Send(new GetTopCategoriesExcelQuery(), ct);
        string fileName = $"Reporte_Categorias_Mas_Solicitadas_{DateTime.Now:yyyyMMdd}.xlsx";
    
        return File(fileBytes, ExcelContentType, fileName);
    }

    [HttpGet("usuarios-top")]
    public async Task<IActionResult> DownloadTopUsersReport(CancellationToken ct)
    {
        var fileBytes = await _mediator.Send(new GetTopUsersExcelQuery(), ct);
        string fileName = $"Reporte_Usuarios_Top_{DateTime.Now:yyyyMMdd}.xlsx";
    
        return File(fileBytes, ExcelContentType, fileName);
    }
}