using AutoMapper;
using ExamSystem.Models;
using ExamSystem.Models.DTOs.AppUser;
using ExamSystem.Models.DTOs.Auth;
using ExamSystem.Models.DTOs.Exam;
using ExamSystem.Models.DTOs.Field;
using ExamSystem.Models.DTOs.FieldExams;
using ExamSystem.Models.DTOs.Option;
using ExamSystem.Models.DTOs.Question;
using ExamSystem.Models.DTOs.UserAnswers;
using ExamSystem.Models.DTOs.UserExams;

namespace ExamSystem.Services.AutoMapperService
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, AppUser>();

            //field mapping
            CreateMap<Field, FieldResponseDto>();
            CreateMap<FieldRequestDto, Field>();

            //exam mapping
            CreateMap<Exam, ExamResponseDto>();
            CreateMap<ExamRequestDto, Exam>();

            //Field Exams Mapping        
            CreateMap<FieldExams, FieldExamsDto>().ReverseMap();

            //question mapping
            CreateMap<Question, QuestionResponseDto>();
            CreateMap<QuestionRequestDto, Question>();

            //option mapping
            CreateMap<Option, OptionResponseDto>();
            CreateMap<OptionRequestDto, Option>();

            //user answers mapping
            CreateMap<UserAnswers, UserAnswerRequestDto>().ReverseMap();
            CreateMap<UserAnswers, UserAnswerResponseDto>().ReverseMap();

            //app user mapping
            CreateMap<AppUser, AppUserRequestDto>().ReverseMap();
            CreateMap<AppUser, AppUserResponseDto>().ReverseMap();
        }
    }
}
