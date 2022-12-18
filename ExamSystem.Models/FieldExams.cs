using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamSystem.Models
{
    public class FieldExams
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int FieldId { get; set; }
        [ForeignKey(nameof(FieldId))]
        public Field Field { get; set; }
        [Required]
        public int ExamId { get; set; }
        [ForeignKey(nameof(ExamId))]
        public Exam Exam { get; set; }
    }
}
