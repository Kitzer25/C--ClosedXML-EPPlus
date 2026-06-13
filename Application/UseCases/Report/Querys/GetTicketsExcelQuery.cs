using ClosedXML.Excel;
using Domain.Ports.Repositories;
using Domain.Ports.Services;

namespace Application.UseCases.Report.Querys;

using MediatR;

public class GetTicketsExcelQuery : IRequest<byte[]> { }

internal sealed class GetTicketsExcelQueryHandler(
    IUnitOfWork unitOfWork, 
    IExcelExportService excelService) 
    : IRequestHandler<GetTicketsExcelQuery, byte[]>
{
    public async Task<byte[]> Handle(GetTicketsExcelQuery request, CancellationToken ct)
    {
        var tickets = await unitOfWork.TicketRepo.GetAllAsync(ct);

        var reportData = tickets.Select(t => new
        {
            t.TicketId,
            t.UserId,
            t.Title,
            t.Description,
            t.Status,
            t.CreatedAt,
            t.ClosedAt,
            t.CategoryId
        }).ToList();

        // 2. Delegar la construcción del Excel al servicio por medio de su interfaz
        byte[] excelFile = excelService.ExportToTable(reportData, "Tickets");

        return excelFile;
    }
}