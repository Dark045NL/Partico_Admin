using System.ComponentModel.DataAnnotations;

namespace KE03_INTDEV_SE_2_Base.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Gebruikersnaam is verplicht.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht.")]
        public string Password { get; set; }

        public LoginViewModel()
        {
            Username = string.Empty;
            Password = string.Empty;
        }

        public string? ReturnUrl { get; set; }
    }
}
