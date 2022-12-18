using ExamSystem.Models.DTOs.Exam;
using ExamSystem.Models.DTOs.Field;
using System.Text.Json.Serialization;

namespace ExamSystem.Models.DTOs.FieldExams
{
    public class FieldExamsDto
    {
        public int Id { get; set; }
        public int FieldId { get; set; }
        [JsonIgnore]
        public FieldResponseDto Field { get; set; }
        public int ExamId { get; set; }
        [JsonIgnore]
        public ExamResponseDto Exam { get; set; }
    }
}
