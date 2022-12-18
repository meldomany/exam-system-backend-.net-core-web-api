using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ExamSystem.Models.DTOs.FieldExams;

namespace ExamSystem.Models.DTOs.Exam
{
    public class ExamRequestDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Duration { get; set; }
        public IEnumerable<FieldExamsDto> FieldExams { get; set; }
    }
}
