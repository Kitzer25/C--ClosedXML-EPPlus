using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Category.Commands;

public class UpdateCategoryCommand : IRequest<string>
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

internal sealed class UpdateCategoryCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateCategoryCommand, string>
{
    public async Task<string> Handle(UpdateCategoryCommand request, CancellationToken ct)
    {
        // Nota: Si tu GetByIdAsync recibe int, recuerda cambiarlo a Guid en tu interfaz genérica
        var category = await unitOfWork.CategoryRepo.GetByIdAsync(request.CategoryId, ct);
        
        if (category == null)
        {
            throw new ApplicationException("La categoría no existe");
        }

        category.Name = request.Name;
        category.Description = request.Description;

        await unitOfWork.CategoryRepo.UpdateAsync(request.CategoryId, category, ct);

        return "Categoría actualizada exitosamente";
    }
}