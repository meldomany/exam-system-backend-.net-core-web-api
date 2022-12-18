using ExamSystem.DataAccess.IRepository;
using ExamSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSystem.DataAccess.Repository
{
    public class ExamRepository : IExamRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ExamRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateExamAsync(Exam exam)
        {
            if (await IsExist(exam)) return false;

            if (exam.FieldExams != null)
            {
                if (exam.FieldExams.Count() > 0)
                {
                    var fieldExamDb = await dbContext.FieldExams.Where(fe => fe.ExamId == exam.Id).ToListAsync();
                    if (fieldExamDb.Count > 0) dbContext.FieldExams.RemoveRange(fieldExamDb);
                    dbContext.FieldExams.AddRange(exam.FieldExams);
                }
                else
                {
                    var fieldExamDb = await dbContext.FieldExams.Where(fe => fe.ExamId == exam.Id).ToListAsync();
                    if (fieldExamDb.Count > 0) dbContext.FieldExams.RemoveRange(fieldExamDb);
                }
            }

            await dbContext.Exams.AddAsync(exam);
            return true;
        }

        public async Task<bool> CreateExamsAsync(List<Exam> exams)
        {
            foreach (var exam in exams)
            {
                if (await IsExist(exam)) return false;
                exam.CreatedAt = DateTime.Now;
                if (exam.FieldExams != null)
                {
                    if (exam.FieldExams.Count() > 0)
                    {
                        var fieldExamDb = await dbContext.FieldExams.Where(fe => fe.ExamId == exam.Id).ToListAsync();
                        if (fieldExamDb.Count > 0) dbContext.FieldExams.RemoveRange(fieldExamDb);
                        dbContext.FieldExams.AddRange(exam.FieldExams);
                    }
                    else
                    {
                        var fieldExamDb = await dbContext.FieldExams.Where(fe => fe.ExamId == exam.Id).ToListAsync();
                        if (fieldExamDb.Count > 0) dbContext.FieldExams.RemoveRange(fieldExamDb);
                    }
                }
            }

            await dbContext.Exams.AddRangeAsync(exams);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var exam = await GetByIdAsync(id);
            if (exam == null) return false;

            var userExams = await dbContext.UserExams.Where(ue => ue.ExamId == id).Select(ue => ue.Id).ToListAsync();
            var userAnswers = await dbContext.UserAnswers.Where(ua => userExams.Contains(ua.UserExamsId)).ToListAsync();
            var userExamsList = await dbContext.UserExams.Where(ue => ue.ExamId == id).ToListAsync();

            dbContext.UserAnswers.RemoveRange(userAnswers);
            dbContext.UserExams.RemoveRange(userExamsList);
            dbContext.Exams.Remove(exam);

            return true;
        }

        public async Task<Exam> GetByIdAsync(int id)
        {
            var exam = await dbContext.Exams
                .Include(e => e.FieldExams)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (exam == null) return null;
            return exam;
        }

        public async Task<IEnumerable<Exam>> GetExamsAsync()
        {
            var exams = await dbContext.Exams
                .Include(e => e.FieldExams)
                .Include(e => e.Questions)
                .ToListAsync();
            return exams;
        }

        public async Task<IEnumerable<Exam>> GetFieldExams(int fieldId)
        {
            var fieldExams = await dbContext.FieldExams
                .Where(fe => fe.FieldId == fieldId)
                .Include(fe => fe.Exam)
                .ToListAsync();

            var exams = fieldExams.Select(fe => fe.Exam).ToList();
            return exams;
        }

        public async Task<bool> IsExist(Exam exam)
        {
            if (await dbContext.Exams.AnyAsync(f => f.Name == exam.Name))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateExamAsync(Exam exam)
        {
            var exams = await dbContext.Exams.Where(f => f.Id != exam.Id).ToListAsync();
            foreach (var examDb in exams)
            {
                if (examDb.Name == exam.Name) return false;
            }

            if (exam.FieldExams.Count() > 0)
            {
                var fieldExamDb = await dbContext.FieldExams.Where(fe => fe.ExamId == exam.Id).ToListAsync();
                if (fieldExamDb.Count > 0) dbContext.FieldExams.RemoveRange(fieldExamDb);
                dbContext.FieldExams.AddRange(exam.FieldExams);
            }
            else
            {
                var fieldExamDb = await dbContext.FieldExams.Where(fe => fe.ExamId == exam.Id).ToListAsync();
                if (fieldExamDb.Count > 0) dbContext.FieldExams.RemoveRange(fieldExamDb);
            }

            dbContext.Exams.Update(exam);
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}