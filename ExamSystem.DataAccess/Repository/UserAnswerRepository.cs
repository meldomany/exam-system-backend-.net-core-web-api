using ExamSystem.DataAccess.IRepository;
using ExamSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSystem.DataAccess.Repository
{
    public class UserAnswerRepository : IUserAnswerRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserAnswerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateUserAnswers(List<UserAnswers> userAnswers)
        {
            await dbContext.UserAnswers.AddRangeAsync(userAnswers);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<UserAnswers>> GetUserAnswersByUserExamId(int userExamId)
        {
            var userAnswers = await dbContext.UserAnswers.Where(ua => ua.UserExamsId == userExamId)
                .Include(ua => ua.Question).Include(ua => ua.Option).ToListAsync();
            return userAnswers;
        }
    }
}