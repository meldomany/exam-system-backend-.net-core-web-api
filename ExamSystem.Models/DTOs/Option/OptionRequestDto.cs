namespace ExamSystem.Models.DTOs.Option
{
    public class OptionRequestDto
    {
        public int Id { get; set; }
        public string OptionName { get; set; }
        public bool isCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}