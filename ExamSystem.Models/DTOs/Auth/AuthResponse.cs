using Microsoft.AspNetCore.Identity;

namespace ExamSystem.Models.DTOs.Auth
{
    public class AuthResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string RoleName { get; set; }
        public string Token { get; set; }
    }
}
