using ExamSystem.Models.DTOs.Option;
using ExamSystem.Models.DTOs.Question;
using System.Collections.Generic;

namespace ExamSystem.Models.DTOs.ExamStarter
{
    public class ExamImplementationViewDto
    {
        public ExamImplementationViewDto()
        {
            Options = new List<OptionResponseDto>();
        }
        public int UserExamId { get; set; }
        public QuestionResponseDto Question { get; set; }
        public OptionResponseDto OptionSelected { get; set; }
        public List<OptionResponseDto> Options { get; set; }
    }
}