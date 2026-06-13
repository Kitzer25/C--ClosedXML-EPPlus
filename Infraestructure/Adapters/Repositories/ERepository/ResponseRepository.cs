using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepository;

public class ResponseRepository :
    GRepositories<Response>,
    IResponseRepository
{
    public ResponseRepository(AppDbContext context) : base(context)
    {
    }
}
