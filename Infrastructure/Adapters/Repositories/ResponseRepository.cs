using Domain.Ports.Repositories.ResponseRepository;
using Domain.Entities;

namespace Infrastructure.Adapters.Repositories.ResponseRepository;

public class ResponseRepository :
    GRepositories<Response>,
    IResponseRepository
{
    public ResponseRepository(AppDbContext context) : base(context)
    {
    }
}
