using ExamSystem.Models.DTOs.Exam;
using ExamSystem.Models.DTOs.Option;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ExamSystem.Models.DTOs.Question
{
    public class QuestionResponseDto
    {
        public int Id { get; set; }
        public string QuestionName { get; set; }
        public double Degree { get; set; }
        public int ExamId { get; set; }
        public ExamResponseDto Exam { get; set; }
        [JsonIgnore]
        public IEnumerable<OptionResponseDto> Options { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
