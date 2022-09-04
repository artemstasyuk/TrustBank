using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BankApplication.Models;

public enum ProfileStatus
{
    Active,
    Deleted,
    Returned
}

public class Profile
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    
    
    
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    
    [StringLength(20)]
    public string Name { get; set; }

    
    [StringLength(20)]
    public string Surname { get; set; }

    public List<Card> Cards { get; set; } = new();
    
    public ProfileStatus Status { get; set; }

    public bool IsVerified { get; set; } = false;
    
    public User User { get; set; }
}