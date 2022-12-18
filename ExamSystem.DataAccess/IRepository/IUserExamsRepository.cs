using ExamSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamSystem.DataAccess.IRepository
{
    public interface IUserExamsRepository
    {
        Task<int> CreateUserExamsAsync(UserExams userExam);
        Task<List<UserExams>> GetAuthUserExamsAsync(string userId);
        Task<UserExams> GetByIdAsync(int userExamId);
        Task<bool> DeleteUserExam(int userExamId);
        Task<List<UserExams>> GetUserExamsAsync();
    }
}
