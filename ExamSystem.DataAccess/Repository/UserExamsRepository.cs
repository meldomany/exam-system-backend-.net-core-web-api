using ExamSystem.DataAccess.IRepository;
using ExamSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSystem.DataAccess.Repository
{
    public class UserExamsRepository : IUserExamsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserExamsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserExams> GetByIdAsync(int userExamId)
        {
            return await dbContext.UserExams.Include(ue => ue.Exam).FirstOrDefaultAsync(ue => ue.Id == userExamId);
        }

        public async Task<int> CreateUserExamsAsync(UserExams userExams)
        {
            await dbContext.UserExams.AddAsync(userExams);
            await dbContext.SaveChangesAsync();
            return userExams.Id;
        }

        public async Task<List<UserExams>> GetAuthUserExamsAsync(string userId)
        {
            return await dbContext.UserExams.Where(ue => ue.UserId == userId)
                .Include(ue => ue.Exam)
                .ToListAsync();
        }

        public async Task<bool> DeleteUserExam(int userExamId)
        {
            var userExam = await dbContext.UserExams.FindAsync(userExamId);

            var userAnswers = await dbContext.UserAnswers.Where(ua => ua.UserExamsId == userExamId).ToListAsync();
            dbContext.UserAnswers.RemoveRange(userAnswers);

            dbContext.UserExams.Remove(userExam);

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserExams>> GetUserExamsAsync()
        {
            return await dbContext.UserExams
                .Include(ue => ue.AppUser)
                .Include(ue => ue.Exam)
                .ToListAsync();
        }
    }
}
