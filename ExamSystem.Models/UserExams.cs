using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamSystem.Models
{
    public class UserExams
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ExamId { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser AppUser { get; set; }
        
        [ForeignKey(nameof(ExamId))]
        public Exam Exam { get; set; }
    }
}