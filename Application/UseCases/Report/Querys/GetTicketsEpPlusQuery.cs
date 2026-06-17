using Domain.DTO_s;
using Domain.Ports.Repositories;
using Domain.Ports.Services;
using MediatR;

namespace Application.UseCases.Report.Querys;

public class GetTicketsEpPlusQuery : IRequest<byte[]> { }

internal sealed class GetTicketsEpPlusHandler(
    IUnitOfWork unitOfWork, 
    IExcelExportService excelService) 
    : IRequestHandler<GetTicketsExcelQuery, byte[]>
{
    public async Task<byte[]> Handle(GetTicketsExcelQuery request, CancellationToken ct)
    {
        var tickets = await unitOfWork.TicketRepo.GetAllAsync(ct);

        // 2. Mapeamos hacia el DTO explícito en lugar del tipo anónimo
        var reportData = tickets.Select(t => new TicketReportDto(
            t.TicketId,
            t.UserId,
            t.Title,
            t.Description,
            t.Status,
            t.CreatedAt,
            t.ClosedAt,
            t.CategoryId
        )).ToList();

        // 3. El servicio procesará el DTO perfectamente gracias a Reflection público
        byte[] excelFile = excelService.ExportToTable(reportData, "Tickets");

        return excelFile;
    }
}