using ExamSystem.Models.DTOs.Question;

namespace ExamSystem.Models.DTOs.Option
{
    public class OptionResponseDto
    {
        public int Id { get; set; }
        public string OptionName { get; set; }
        public bool isCorrect { get; set; }
        public int QuestionId { get; set; }
        public QuestionResponseDto Question { get; set; }
        public string CreatedAt { get; set; }
    }
}