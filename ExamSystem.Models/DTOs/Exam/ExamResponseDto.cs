using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ExamSystem.Models.DTOs.FieldExams;
using System.Text.Json.Serialization;
using ExamSystem.Models.DTOs.Question;

namespace ExamSystem.Models.DTOs.Exam
{
    public class ExamResponseDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<FieldExamsDto> FieldExams { get; set; }
        [JsonIgnore]
        public IEnumerable<QuestionResponseDto> Questions { get; set; }
    }
}
