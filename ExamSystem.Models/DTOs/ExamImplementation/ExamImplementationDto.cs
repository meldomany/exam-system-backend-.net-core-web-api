using ExamSystem.Models.DTOs.Option;
using ExamSystem.Models.DTOs.Question;
using System.Collections.Generic;

namespace ExamSystem.Models.DTOs.ExamStarter
{
    public class ExamImplementationDto
    {
        public ExamImplementationDto()
        {
            Options = new List<OptionResponseDto>();
        }
        public QuestionResponseDto Question { get; set; }
        public List<OptionResponseDto> Options { get; set; }
    }
}