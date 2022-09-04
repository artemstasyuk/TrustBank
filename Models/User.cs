using System.ComponentModel.DataAnnotations;

namespace BankApplication.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    [StringLength(6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string Email { get; set; }
    
}