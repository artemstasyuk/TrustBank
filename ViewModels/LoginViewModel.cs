using System.ComponentModel.DataAnnotations;

namespace BankApplication.ViewModels;

public class LoginViewModel
{

    [EmailAddress(ErrorMessage = "Email must match the format")]
    [Required(ErrorMessage = "Enter your email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Enter a password")]
    [DataType(DataType.Password)]
    [StringLength(6, ErrorMessage = "Password consists of 6 characters", MinimumLength = 6)]
    public string Password { get; set; }
}