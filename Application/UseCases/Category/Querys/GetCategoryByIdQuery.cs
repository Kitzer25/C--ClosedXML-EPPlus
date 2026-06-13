using Domain.DTO_s;
using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Category.Querys;

public class GetCategoryByIdQuery : IRequest<CategoryDto>
{
    public Guid CategoryId { get; set; }
}
 
internal sealed class GetCategoryByIdQueryHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken ct)
    {
        var category = await unitOfWork.CategoryRepo.GetByIdAsync(request.CategoryId, ct)
                       ?? throw new KeyNotFoundException("La categoría no existe");
 
        return new CategoryDto(
            category.CategoryId,
            category.Name,
            category.Description,
            category.Tickets.Count);
    }
}