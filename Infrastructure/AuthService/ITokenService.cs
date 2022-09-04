namespace BankApplication.Infrastructure.AuthService;

public interface ITokenService
{
    string CreateToken(User user);
}