using System.ComponentModel.DataAnnotations;

namespace BankApplication.ViewModels
{
    public enum Currency
    {
        Ruble,
        Dollar,
        Euro
    }

    
    public class UserViewModel
    {
        [Display(Name = "Name")]
        [StringLength(40)]
        [Required(ErrorMessage = "Username must match the format")]
        public string UserName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(13)]
        [Required(ErrorMessage = "Phone number must match the format")]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(60)]
        [Required(ErrorMessage = "Email must match the format")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [StringLength(15)]
        [Required(ErrorMessage = "Date must match the format")]
        public string DateOfBirth { get; set; }
        
        public string Citizenship { get; set; }
        
        public Currency Сurrency { get; set; }

    }
}
