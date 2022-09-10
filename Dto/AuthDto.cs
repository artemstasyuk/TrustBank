using NuGet.Common;

namespace BankApplication.Dto;

public class AuthDto
{
    public string  Token { get; set; }
    public string Error { get; set; } = string.Empty;

    public bool Status { get; set; } = true;
}