using System.ComponentModel.DataAnnotations;

namespace BankApplication.ViewModels;

public class CheckoutViewModel
{
    [StringLength(30, ErrorMessage = "Name must match the format")]
    [Required(ErrorMessage = "Enter your name")]
    public string Name { get; set; }

    [StringLength(50, ErrorMessage = "Surname must match the format")]
    [Required(ErrorMessage = "Enter your surname")]
    public string Surname { get; set; }

    [EmailAddress(ErrorMessage = "Email must match the format")]
    [Required(ErrorMessage = "Enter your email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Please enter your adress")]
    public string Adress { get; set; }

}