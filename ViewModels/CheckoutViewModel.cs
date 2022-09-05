using System.ComponentModel.DataAnnotations;

namespace BankApplication.ViewModels;

public class CheckoutViewModel
{
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    public string Adress { get; set; }

}