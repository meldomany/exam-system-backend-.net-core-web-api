using AutoMapper;
using ExamSystem.DataAccess.IRepository;
using ExamSystem.Models.DTOs.ExamStarter;
using ExamSystem.Models.DTOs.Option;
using ExamSystem.Models.DTOs.Question;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSystem.DataAccess.Repository
{
    public class ExamImplementationRepository : IExamImplementationRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public ExamImplementationRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<ExamImplementationDto>> GetExamImplementation(int examId)
        {
            var examImplementationDto = new List<ExamImplementationDto>();

            var questions = await dbContext.Questions.Where(q => q.ExamId == examId).ToListAsync();
            foreach (var question in questions)
            {
                var options = await dbContext.Options.Where(o => o.QuestionId == question.Id).ToListAsync();
                var optionsDto = mapper.Map<List<OptionResponseDto>>(options);

                examImplementationDto.Add(new ExamImplementationDto()
                {
                    Question = mapper.Map<QuestionResponseDto>(question),
                    Options = optionsDto
                });
            }

            return examImplementationDto;
        }
    }
}
