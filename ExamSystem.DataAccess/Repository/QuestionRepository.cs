using QuestionSystem.DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamSystem.Models;
using ExamSystem.DataAccess;
using System;

namespace QuestionSystem.DataAccess.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext dbContext;

        public QuestionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateQuestionAsync(Question question)
        {
            if (await IsExist(question)) return false;
            question.CreatedAt = DateTime.Now;
            await dbContext.Questions.AddAsync(question);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var question = await GetByIdAsync(id);
            if (question == null) return false;
            dbContext.Questions.Remove(question);
            return true;
        }

        public async Task<Question> GetByIdAsync(int id)
        {
            var question = await dbContext.Questions
                .Include(q => q.Exam)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (question == null) return null;
            return question;
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            var questions = await dbContext.Questions
                .Include(q => q.Exam)
                .ToListAsync();
            return questions;
        }

        public async Task<bool> IsExist(Question question)
        {
            if (await dbContext.Questions.AnyAsync(q => q.QuestionName == question.QuestionName))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateQuestionAsync(Question question)
        {
            var questions = await dbContext.Questions.Where(f => f.Id != question.Id).ToListAsync();
            foreach (var questionDb in questions)
            {
                if (questionDb.QuestionName == question.QuestionName) return false;
            }

            dbContext.Questions.Update(question);
            return true;
        }

        public async Task<IEnumerable<Question>> GetExamQuestions(int id)
        {
            return await dbContext.Questions.Where(q => q.ExamId == id).ToListAsync();
        }

        public async Task<bool> DeleteByExamIdAsync(int examId)
        {
            var questions = await dbContext.Questions.Where(q => q.ExamId == examId).ToListAsync();
            dbContext.Questions.RemoveRange(questions);
            return true;
        }

        public async Task<bool> CreateQuestionsAsync(List<Question> questions)
        {
            foreach (var question in questions)
            {
                if (await IsExist(question)) return false;
                question.CreatedAt = DateTime.Now;
            }
            await dbContext.Questions.AddRangeAsync(questions);
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}