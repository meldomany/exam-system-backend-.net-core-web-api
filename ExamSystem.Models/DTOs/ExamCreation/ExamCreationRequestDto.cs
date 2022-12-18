using System.Collections.Generic;

namespace ExamSystem.Models.DTOs.ExamCreation
{
    public class ExamCreationRequestDto
    {
        public ExamCreationRequestDto()
        {
            Questions = new List<ExamCreationQuestions>();
        }

        public string ExamName { get; set; }
        public string ExamShortDescription { get; set; }
        public string ExamDescription { get; set; }
        public int ExamDuration { get; set; }
        public List<examCreationFieldExams> FieldExams { get; set; }
        public List<ExamCreationQuestions> Questions { get; set; }
    }

    public class examCreationFieldExams
    {
        public int FieldId { get; set; }
        public int ExamId { get; set; }
    }
}
