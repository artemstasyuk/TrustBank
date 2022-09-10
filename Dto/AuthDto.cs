
namespace BankApplication.Dto;

public class AuthErrorDto
{
    public string  Token { get; set; }
    public string Error { get; set; } = string.Empty;
    public bool Status { get; set; }
}