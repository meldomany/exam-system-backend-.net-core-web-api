using ExamSystem.Models.DTOs.Exam;
using System.Collections.Generic;

namespace ExamSystem.Models.DTOs.UserExams
{
    public class UserExamsResponseDto
    {
        public UserExamsResponseDto()
        {
            Exams = new List<ExamResponseDto>();
        }
        public List<ExamResponseDto> Exams { get; set; }
    }
}
