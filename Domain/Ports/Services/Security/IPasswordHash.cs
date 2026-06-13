namespace Domain.Ports.Services.Security;

public interface IPasswordHash
{
    string Hash(string password);
}