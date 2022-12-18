using ExamSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamSystem.DataAccess.IRepository
{
    public interface IFieldRepository
    {
        Task<IEnumerable<Field>> GetFieldsAsync();
        Task<Field> GetByIdAsync(int id);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> CreateFieldAsync(Field field);
        Task<bool> CreateFieldsAsync(List<Field> field);
        Task<bool> UpdateFieldAsync(Field field);
        Task<bool> IsExist(Field field);
        Task<bool> SaveChangesAsync();
    }
}
