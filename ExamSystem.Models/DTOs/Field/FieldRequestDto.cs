﻿using System;

namespace ExamSystem.Models.DTOs.Field
{
    public class FieldRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
    }
}
