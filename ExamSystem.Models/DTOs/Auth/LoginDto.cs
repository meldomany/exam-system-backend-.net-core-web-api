using System.ComponentModel.DataAnnotations;

namespace ExamSystem.Models.DTOs.Auth
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(16, MinimumLength = 8)]
        public string PasswordHash { get; set; }
    }
}
