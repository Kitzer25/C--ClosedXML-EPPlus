using Domain.DTO_s;
using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Category.Querys;

public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
{
}
 
internal sealed class GetAllCategoriesQueryHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
{
    public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken ct)
    {
        var categories = await unitOfWork.CategoryRepo.GetAllWithTicketsAsync(ct);
 
        return categories.Select(c => new CategoryDto(
            c.CategoryId,
            c.Name,
            c.Description,
            c.Tickets.Count));
    }
}