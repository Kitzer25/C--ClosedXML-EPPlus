using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Category.Commands;

public class DeleteCategoryCommand : IRequest<string>
{
    public Guid CategoryId { get; set; }
}

internal sealed class DeleteCategoryCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteCategoryCommand, string>
{
    public async Task<string> Handle(DeleteCategoryCommand request, CancellationToken ct)
    {
        var category = await unitOfWork.CategoryRepo.GetByIdAsync(request.CategoryId, ct);

        if (category == null)
        {
            throw new ApplicationException("La categoría no existe");
        }

        await unitOfWork.CategoryRepo.DeleteAsync(category, ct);

        return "Categoría eliminada exitosamente";
    }
}