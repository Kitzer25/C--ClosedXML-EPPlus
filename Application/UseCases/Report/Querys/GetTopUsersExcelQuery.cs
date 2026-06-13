using Domain.Ports.Repositories;
using Domain.Ports.Services;
using MediatR;

namespace Application.UseCases.Report.Querys;

public class GetTopUsersExcelQuery : IRequest<byte[]> { }

internal sealed class GetTopUsersExcelQueryHandler(
    IUnitOfWork unitOfWork, 
    IExcelExportService excelService) 
    : IRequestHandler<GetTopUsersExcelQuery, byte[]>
{
    public async Task<byte[]> Handle(GetTopUsersExcelQuery request, CancellationToken ct)
    {
        var reportData = await unitOfWork.TicketRepo.GetTopUsersAsync(topCount: 50, ct);

        return excelService.ExportToTable(reportData, "Usuarios Top");
    }
}