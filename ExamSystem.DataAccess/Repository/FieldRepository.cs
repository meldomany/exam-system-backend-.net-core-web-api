using ExamSystem.DataAccess.IRepository;
using ExamSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSystem.DataAccess.Repository
{
    public class FieldRepository : IFieldRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IExamRepository examRepository;

        public FieldRepository(ApplicationDbContext dbContext, IExamRepository examRepository)
        {
            this.dbContext = dbContext;
            this.examRepository = examRepository;
        }

        public async Task<bool> CreateFieldAsync(Field field)
        {
            if (await IsExist(field)) return false;
            field.CreatedAt = DateTime.Now;
            await dbContext.Fields.AddAsync(field);
            return true;
        }

        public async Task<bool> CreateFieldsAsync(List<Field> fields)
        {
            foreach (var field in fields)
            {
                if (await IsExist(field)) return false;
                field.CreatedAt = DateTime.Now;
            }

            await dbContext.Fields.AddRangeAsync(fields);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var field = await GetByIdAsync(id);
            if (field == null) return false;
            dbContext.Fields.Remove(field);
            return true;
        }

        public async Task<Field> GetByIdAsync(int id)
        {
            var field = await dbContext.Fields.Include(f => f.FieldExams).ThenInclude(fe => fe.Exam).FirstOrDefaultAsync(f => f.Id == id);
            if (field == null) return null;
            return field;
        }

        public async Task<IEnumerable<Field>> GetFieldsAsync()
        {
            var fields = await dbContext.Fields.Include(f => f.FieldExams).ToListAsync();
            return fields;
        }

        public async Task<bool> IsExist(Field field)
        {
            if (await dbContext.Fields.AnyAsync(f => f.Name == field.Name)) return true;
            return false;
        }

        public async Task<bool> UpdateFieldAsync(Field field)
        {
            var fields = await dbContext.Fields.Where(f => f.Id != field.Id).ToListAsync();
            foreach (var fieldDb in fields)
            {
                if (fieldDb.Name == field.Name) return false;
            }

            dbContext.Fields.Update(field);
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
