namespace BankApplication.Infrastructure.AuthService.UserControlService;

public interface IUserControlService
{
    Task<AuthErrorDto> Login(LoginViewModel viewModel);
    Task<AuthErrorDto> Registration(RegistrationViewModel viewModel);
    Task<AuthErrorDto> VerifyEmailToken(EmailViewModel emailDto);
}