namespace BankApplication.Infrastructure.AuthService.UserControlService;

public interface IUserControlService
{
    Task<AuthDto> Login(LoginViewModel viewModel);
    Task<AuthDto> Registration(RegistrationViewModel viewModel);
    Task<AuthDto> VerifyEmailToken(EmailViewModel emailDto);
}