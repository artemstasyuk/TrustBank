using System.ComponentModel.DataAnnotations;

namespace BankApplication.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public string Password { get; set; }

    public string Email { get; set; }
    
    public bool IsVerified { get; set; } = false;
    
}