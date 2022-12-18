using ExamSystem.Models.DTOs.Option;
using ExamSystem.Models.DTOs.Question;

namespace ExamSystem.Models.DTOs.UserAnswers
{
    public class UserAnswerResponseDto
    {
        public int UserExamsId { get; set; }
        public int QuestionId { get; set; }
        public int OptionId { get; set; }
        public QuestionResponseDto Question { get; set; }
        public OptionResponseDto Option { get; set; }
    }
}
