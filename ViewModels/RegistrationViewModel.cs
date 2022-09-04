using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApplication.Dto;

public class RegistrationViewModel
{

    [Display(Name = "Password")]
    [StringLength(6)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Name must match the format")]
    public string Password { get; set; }
    
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Name must match the format")]
    public string Email { get; set; }
    
    
    [StringLength(20)]
    [Required(ErrorMessage = "Name must match the format")]
    public string AccountName { get; set; }

    
    [StringLength(20)]
    [Required(ErrorMessage = "Surname must match the format")] 
    public string AccountSurname { get; set; }
    
    [DataType(DataType.Password)]
    [StringLength(6)]
    [Compare("Password", ErrorMessage = "Password entered incorrectly")]
    public string ConfirmPassword { get; set; }
}