namespace Domain.DTO_s;

public sealed record UserDto(
    Guid UserId,
    string Username,
    string? Email,
    DateTime? CreatedAt,
    IReadOnlyList<string> Roles);