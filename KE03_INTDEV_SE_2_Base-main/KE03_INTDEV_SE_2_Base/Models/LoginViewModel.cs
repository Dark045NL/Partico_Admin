using System.ComponentModel.DataAnnotations;

namespace KE03_INTDEV_SE_2_Base.Models
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginViewModel()
        {
            Username = string.Empty;
            Password = string.Empty;
        }

        public string? ReturnUrl { get; set; }
    }
}
