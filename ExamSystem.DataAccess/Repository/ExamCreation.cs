using ExamSystem.DataAccess.IRepository;
using ExamSystem.Models;
using ExamSystem.Models.DTOs.ExamCreation;
using OptionSystem.DataAccess.IRepository;
using QuestionSystem.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamSystem.DataAccess.Repository
{
    public class ExamCreation : IExamCreation
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IExamRepository examRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly IOptionRepository optionRepository;

        public ExamCreation(ApplicationDbContext dbContext,
            IExamRepository examRepository,
            IQuestionRepository questionRepository,
            IOptionRepository optionRepository)
        {
            this.dbContext = dbContext;
            this.examRepository = examRepository;
            this.questionRepository = questionRepository;
            this.optionRepository = optionRepository;
        }

        public async Task<string> ExamCreationAsync(ExamCreationRequestDto examCreationRequestDto)
        {
            var fieldExams = new List<FieldExams>();

            if (examCreationRequestDto.FieldExams.Count > 0)
            {
                foreach (var examCreationFieldExam in examCreationRequestDto.FieldExams)
                {
                    fieldExams.Add(new FieldExams
                    {
                        FieldId = examCreationFieldExam.FieldId,
                        ExamId = examCreationFieldExam.ExamId
                    });
                }
            }

            //creation of exam
            var exam = new Exam
            {
                Name = examCreationRequestDto.ExamName,
                ShortDescription = examCreationRequestDto.ExamShortDescription,
                Description = examCreationRequestDto.ExamDescription,
                Duration = examCreationRequestDto.ExamDuration,
                FieldExams = fieldExams,
                CreatedAt = DateTime.Now
            };

            if (await examRepository.CreateExamAsync(exam))
            {
                await SaveChangesAsync();

                foreach (var questionDto in examCreationRequestDto.Questions)
                {
                    var question = new Question
                    {
                        QuestionName = questionDto.QuestionName,
                        Degree = questionDto.QuestionDegree,
                        ExamId = exam.Id,
                        CreatedAt = DateTime.Now
                    };

                    if (await questionRepository.CreateQuestionAsync(question))
                    {
                        await dbContext.SaveChangesAsync();
                        foreach (var optionDto in questionDto.Options)
                        {
                            var option = new Option
                            {
                                OptionName = optionDto.OptionName,
                                isCorrect = optionDto.OptionIsCorrect,
                                QuestionId = question.Id,
                                CreatedAt = DateTime.Now
                            };

                            if (await optionRepository.CreateOptionAsync(option))
                            {
                                await SaveChangesAsync();
                            }
                            else
                            {
                                await examRepository.DeleteByIdAsync(exam.Id);
                                await questionRepository.DeleteByExamIdAsync(exam.Id);
                                await SaveChangesAsync();

                                return option.OptionName + " option is already exists";
                            }
                        }
                    }
                    else
                    {
                        await examRepository.DeleteByIdAsync(exam.Id);
                        await SaveChangesAsync();

                        return question.QuestionName + " question is already exists";
                    }
                }
                return "created";
            }
            return "This exam name is already exists";
        }

        public async Task<bool> SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
            return true;
        }

    }
}
