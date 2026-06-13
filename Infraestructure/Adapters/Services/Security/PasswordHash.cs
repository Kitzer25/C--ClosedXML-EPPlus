using Domain.Ports.Services.Security;

namespace Infraestructure.Adapters.Services.Security;

public class PasswordHash : IPasswordHash
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string hash, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}