using Domain.DTO_s;
using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.User.Querys;

public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
{
}
 
internal sealed class GetAllUsersQueryHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken ct)
    {
        var users = await unitOfWork.UserRepo.GetAllAsync(ct);
 
        return users.Select(u => new UserDto(
            u.UserId,
            u.Username,
            u.Email,
            u.CreatedAt,
            u.UserRoles.Select(ur => ur.Role.RoleName).ToList()));
    }
}