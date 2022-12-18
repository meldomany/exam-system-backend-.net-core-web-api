using ExamSystem.Models.DTOs.ExamCreation;
using System.Threading.Tasks;

namespace ExamSystem.DataAccess.IRepository
{
    public interface IExamCreation
    {
        Task<string> ExamCreationAsync(ExamCreationRequestDto examCreationRequestDto);
        Task<bool> SaveChangesAsync();
    }
}
