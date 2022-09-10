using Microsoft.Build.Framework;

namespace BankApplication.ViewModels
{
    public class ProfileViewModel
    {
        public IFormFile PhotoProfile { get; set; }
        
        public Profile Profile { get; set; }
    }
}
