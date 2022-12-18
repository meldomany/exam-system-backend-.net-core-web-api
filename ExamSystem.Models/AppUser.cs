using Microsoft.AspNetCore.Identity;

namespace ExamSystem.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string JobTitle { get; set; }
    }
}
