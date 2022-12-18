using System.ComponentModel.DataAnnotations.Schema;

namespace ExamSystem.Models
{
    public class UserAnswers
    {
        public int Id { get; set; }
        public int UserExamsId { get; set; }
        public int QuestionId { get; set; }
        public int OptionId { get; set; }

        [ForeignKey(nameof(UserExamsId))]
        public UserExams UserExams { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }

        [ForeignKey(nameof(OptionId))]
        public Option Option { get; set; }
    }
}
