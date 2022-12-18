using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamSystem.Models
{
    public class Option
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string OptionName { get; set; }
        [Required]
        public bool isCorrect { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
