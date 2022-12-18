using OptionSystem.DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamSystem.Models;
using ExamSystem.DataAccess;
using System;

namespace OptionSystem.DataAccess.Repository
{
    public class OptionRepository : IOptionRepository
    {
        private readonly ApplicationDbContext dbContext;

        public OptionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateOptionAsync(Option option)
        {
            option.CreatedAt = DateTime.Now;
            await dbContext.Options.AddAsync(option);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var option = await GetByIdAsync(id);
            if (option == null) return false;
            dbContext.Options.Remove(option);
            return true;
        }

        public async Task<Option> GetByIdAsync(int id)
        {
            var option = await dbContext.Options
                .Include(o => o.Question)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (option == null) return null;
            return option;
        }

        public async Task<IEnumerable<Option>> GetOptionsAsync()
        {
            var options = await dbContext.Options
               .Include(o => o.Question)
               .ThenInclude(q => q.Exam)
               .ToListAsync();
            return options;
        }

        public async Task<bool> IsExist(Option option)
        {
            if (await dbContext.Options.AnyAsync(q => q.OptionName == option.OptionName))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateOptionAsync(Option option)
        {
            var options = await dbContext.Options.Where(f => f.Id != option.Id).ToListAsync();
            foreach (var optionDb in options)
            {
                if (optionDb.OptionName == option.OptionName) return false;
            }

            dbContext.Options.Update(option);
            return true;
        }

        public async Task<IEnumerable<Option>> GetQuestionOptions(int questionId)
        {
            return await dbContext.Options.Where(o => o.QuestionId == questionId).Include(o => o.Question).ToListAsync();
        }

        public async Task<bool> CreateOptionsAsync(List<Option> options)
        {
            foreach (var option in options)
            {
                option.CreatedAt = DateTime.Now;
            }
            await dbContext.Options.AddRangeAsync(options);
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}