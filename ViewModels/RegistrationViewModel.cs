using System.ComponentModel.DataAnnotations;

namespace BankApplication.Dto;

public class RegistrationViewModel
{
    [Required(ErrorMessage = "Enter a password")]
    [DataType(DataType.Password)]
    [StringLength(6, ErrorMessage = "Password consists of 6 characters", MinimumLength = 6)]
    public string Password { get; set; }


    [EmailAddress(ErrorMessage = "Email must match the format")]
    [Required(ErrorMessage = "Enter your email")]
    public string Email { get; set; }
    
    
    [StringLength(30, ErrorMessage = "Name must match the format")]
    [Required(ErrorMessage = "Enter your name")]
    public string AccountName { get; set; }

    
    [StringLength(50, ErrorMessage = "Surname must match the format")]
    [Required(ErrorMessage = "Enter your surname")] 
    public string AccountSurname { get; set; }

    [Required]
    [DataType(DataType.Password)]   
    [Compare("Password", ErrorMessage = "Password entered incorrectly")]
    public string ConfirmPassword { get; set; }
}