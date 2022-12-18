using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamSystem.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string QuestionName { get; set; }
        [Required]
        public double Degree { get; set; }
        [Required]
        public int ExamId { get; set; }
        
        [ForeignKey(nameof(ExamId))]
        public Exam Exam { get; set; }
        public IEnumerable<Option> options { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
