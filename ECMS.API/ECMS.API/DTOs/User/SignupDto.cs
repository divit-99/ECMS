using ECMS.API.DTOs.Interface;
using System.ComponentModel.DataAnnotations;

namespace ECMS.API.DTOs.User
{
    public class SignupDto : IHasEmail
    {
        [Required, StringLength(255)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;

        public bool IsAdmin { get; set; } = false;
    }
}
