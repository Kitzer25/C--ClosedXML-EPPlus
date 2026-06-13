using Domain.Ports.Repositories;
using Domain.Ports.Services;
using MediatR;

namespace Application.UseCases.Report.Querys;

public class GetTopCategoriesExcelQuery : IRequest<byte[]> { }

internal sealed class GetTopCategoriesExcelQueryHandler(
    IUnitOfWork unitOfWork, 
    IExcelExportService excelService) 
    : IRequestHandler<GetTopCategoriesExcelQuery, byte[]>
{
    public async Task<byte[]> Handle(GetTopCategoriesExcelQuery request, CancellationToken ct)
    {
        var reportData = await unitOfWork.TicketRepo.GetTopCategoriesAsync(topCount: 20, ct);

        return excelService.ExportToTable(reportData, "Categorias Mas Solicitadas");
    }
}