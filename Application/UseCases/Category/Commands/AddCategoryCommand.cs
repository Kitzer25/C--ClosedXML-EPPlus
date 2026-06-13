using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Category.Commands;

public class AddCategoryCommand : IRequest<string>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

internal sealed class AddCategoryCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<AddCategoryCommand, string>
{
    public async Task<string> Handle(AddCategoryCommand request, CancellationToken ct)
    {
        Domain.Entities.Category category = new Domain.Entities.Category
        {
            CategoryId = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description
        };

        await unitOfWork.CategoryRepo.AddAsync(category, ct);

        return "Categoría añadida exitosamente";
    }
}