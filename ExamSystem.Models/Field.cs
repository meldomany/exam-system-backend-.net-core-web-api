using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamSystem.Models
{
    public class Field
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public IEnumerable<FieldExams> FieldExams { get; set; }
    }
}
