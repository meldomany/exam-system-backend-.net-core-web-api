namespace ExamSystem.Models.DTOs.AppUser
{
    public class AppUserRequestDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string JobTitle { get; set; }
        public string RoleName { get; set; }
    }
}
