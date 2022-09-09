namespace BankApplication.Infrastructure.AuthService.JwtTokenService;

public interface ITokenService
{
    string CreateToken(User user);
}