using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamSystem.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Duration { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public IEnumerable<FieldExams> FieldExams { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }
}
