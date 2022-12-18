using ExamSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OptionSystem.DataAccess.IRepository
{
    public interface IOptionRepository
    {
        Task<IEnumerable<Option>> GetOptionsAsync();
        Task<Option> GetByIdAsync(int id);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> CreateOptionAsync(Option option);
        Task<bool> CreateOptionsAsync(List<Option> options);
        Task<bool> UpdateOptionAsync(Option option);
        Task<bool> IsExist(Option option);
        Task<IEnumerable<Option>> GetQuestionOptions(int id);
        Task<bool> SaveChangesAsync();
    }
}
