using ExamSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExamSystem.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<FieldExams> FieldExams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<UserExams> UserExams { get; set; }
        public DbSet<UserAnswers> UserAnswers { get; set; }
    }
}
