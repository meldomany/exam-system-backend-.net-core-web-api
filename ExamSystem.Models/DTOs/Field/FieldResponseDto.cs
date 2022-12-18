using ExamSystem.Models.DTOs.FieldExams;
using System;
using System.Collections.Generic;

namespace ExamSystem.Models.DTOs.Field
{
    public class FieldResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<FieldExamsDto> FieldExams { get; set; }
    }
}