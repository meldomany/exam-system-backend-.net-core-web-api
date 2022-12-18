namespace ExamSystem.Models.DTOs.Question
{
    public class QuestionRequestDto
    {
        public int Id { get; set; }
        public string QuestionName { get; set; }
        public double Degree { get; set; }
        public int ExamId { get; set; }
    }
}
