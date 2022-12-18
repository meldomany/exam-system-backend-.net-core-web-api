using ExamSystem.Models.DTOs.ExamStarter;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamSystem.DataAccess.IRepository
{
    public interface IExamImplementationRepository
    {
        Task<List<ExamImplementationDto>> GetExamImplementation(int id);
    }
}