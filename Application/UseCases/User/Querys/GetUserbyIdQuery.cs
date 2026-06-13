using Domain.DTO_s;
using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.User.Querys;

public class GetUserByIdQuery : IRequest<UserDto>
{
    public Guid UserId { get; set; }
}
 
internal sealed class GetUserByIdQueryHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken ct)
    {
        var user = await unitOfWork.UserRepo.GetByIdAsync(request.UserId, ct)
                   ?? throw new KeyNotFoundException("El usuario no existe");
 
        return new UserDto(
            user.UserId,
            user.Username,
            user.Email,
            user.CreatedAt,
            user.UserRoles.Select(ur => ur.Role.RoleName).ToList());
    }
}