using ExamSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamSystem.DataAccess.IRepository
{
    public interface IUserAnswerRepository
    {
        Task CreateUserAnswers(List<UserAnswers> userAnswers);
        Task<List<UserAnswers>> GetUserAnswersByUserExamId(int userExamId);
    }
}
