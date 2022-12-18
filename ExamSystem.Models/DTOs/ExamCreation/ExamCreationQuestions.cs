using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Models.DTOs.ExamCreation
{
    public class ExamCreationQuestions
    {
        public ExamCreationQuestions()
        {
            Options = new List<ExamCreationQuestionsOptions>();
        }

        public string QuestionName { get; set; }
        public double QuestionDegree { get; set; }
        public List<ExamCreationQuestionsOptions> Options { get; set; }
    }
}
