using Domain.Ports.Repositories.CategoryRepository;
using Domain.Entities;

namespace Infrastructure.Adapters.Repositories.CategoryRepository;

public class CategoryRepository :
    GRepositories<Category>,
    ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}
