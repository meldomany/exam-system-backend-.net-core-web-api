using ExamSystem.DataAccess.IRepository;
using ExamSystem.DataAccess.Repository;
using ExamSystem.Services.AutoMapperService;
using ExamSystem.Services.JwtTokenService;
using Microsoft.Extensions.DependencyInjection;
using OptionSystem.DataAccess.IRepository;
using OptionSystem.DataAccess.Repository;
using QuestionSystem.DataAccess.IRepository;
using QuestionSystem.DataAccess.Repository;

namespace ExamSystem.API.Extentions
{
    public static class ApplicationExtentions
    {
        public static IServiceCollection AddApplicationServicesExtention(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IFieldRepository, FieldRepository>();
            services.AddScoped<IExamRepository, ExamRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IOptionRepository, OptionRepository>();
            services.AddScoped<IExamCreation, ExamCreation>();
            services.AddScoped<IExamImplementationRepository, ExamImplementationRepository>();
            services.AddScoped<IUserExamsRepository, UserExamsRepository>();
            services.AddScoped<IUserAnswerRepository, UserAnswerRepository>();
            return services;
        }
    }
}
