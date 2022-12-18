using ExamSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamSystem.DataAccess.IRepository
{
    public interface IExamRepository
    {
        Task<IEnumerable<Exam>> GetExamsAsync();
        Task<Exam> GetByIdAsync(int id);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> CreateExamAsync(Exam exam);
        Task<bool> CreateExamsAsync(List<Exam> exams);
        Task<bool> UpdateExamAsync(Exam exam);
        Task<bool> IsExist(Exam exam);
        Task<IEnumerable<Exam>> GetFieldExams(int fieldId);
        Task<bool> SaveChangesAsync();

    }
}
