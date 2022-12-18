namespace ExamSystem.Models.DTOs.UserAnswers
{
    public class UserAnswerRequestDto
    {
        public int UserExamsId { get; set; }
        public int QuestionId { get; set; }
        public int OptionId { get; set; }
    }
}
