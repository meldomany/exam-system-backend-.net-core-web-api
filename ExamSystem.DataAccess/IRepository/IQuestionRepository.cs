using ExamSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuestionSystem.DataAccess.IRepository
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetQuestionsAsync();
        Task<Question> GetByIdAsync(int id);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> DeleteByExamIdAsync(int examId);
        Task<bool> CreateQuestionAsync(Question question);
        Task<bool> CreateQuestionsAsync(List<Question> questions);
        Task<bool> UpdateQuestionAsync(Question question);
        Task<bool> IsExist(Question question);
        Task<IEnumerable<Question>> GetExamQuestions(int id);
        Task<bool> SaveChangesAsync();
    }
}
