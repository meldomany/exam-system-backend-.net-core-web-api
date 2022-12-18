using AutoMapper;
using ExamSystem.DataAccess.IRepository;
using ExamSystem.Models;
using ExamSystem.Models.DTOs.Exam;
using ExamSystem.Models.DTOs.ExamStarter;
using ExamSystem.Models.DTOs.Option;
using ExamSystem.Models.DTOs.UserAnswers;
using Microsoft.AspNetCore.Mvc;
using OptionSystem.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamImplementationsController : ControllerBase
    {
        private readonly IExamImplementationRepository examImplementationRepository;
        private readonly IUserAnswerRepository userAnswerRepository;
        private readonly IMapper mapper;
        private readonly IUserExamsRepository userExamsRepository;
        private readonly IExamRepository examRepository;
        private readonly IOptionRepository optionRepository;

        public ExamImplementationsController(IExamImplementationRepository examImplementationRepository,
            IUserAnswerRepository userAnswerRepository,
            IMapper mapper,
            IUserExamsRepository userExamsRepository,
            IExamRepository examRepository,
            IOptionRepository optionRepository)
        {
            this.examImplementationRepository = examImplementationRepository;
            this.userAnswerRepository = userAnswerRepository;
            this.mapper = mapper;
            this.userExamsRepository = userExamsRepository;
            this.examRepository = examRepository;
            this.optionRepository = optionRepository;
        }

        [HttpGet]
        [Route("GetExamImplementations/{id}")]
        public async Task<IActionResult> GetExamImplementations(int id)
        {
           return Ok(await examImplementationRepository.GetExamImplementation(id));
        }

        private async Task<int> CreateUserExams(int examId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToString();
            var userExams = new UserExams
            {
                UserId = userId,
                ExamId = examId,
                CreatedAt = DateTime.Now
            };
            var userExamId = await userExamsRepository.CreateUserExamsAsync(userExams);
            return userExamId;
        }

        [HttpPost]
        [Route("CreateUserAnswers/{examId}")]
        public async Task<IActionResult> CreateUserAnswers(int examId, List<UserAnswerRequestDto> userAnswerRequestDto)
        {
            var userExamId = await CreateUserExams(examId);
            
            foreach (var userAnswer in userAnswerRequestDto)
            {
                userAnswer.UserExamsId = userExamId;
            }

            var userAnswers = mapper.Map<List<UserAnswers>>(userAnswerRequestDto);
            await userAnswerRepository.CreateUserAnswers(userAnswers);
            return StatusCode(201);
        }

        [HttpGet]
        [Route("GetAuthUserExams")]
        public async Task<IActionResult> GetAuthUserExams()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToString();
            var authUserExams = await userExamsRepository.GetAuthUserExamsAsync(userId);
            return Ok(authUserExams);
        }

        [HttpGet]
        [Route("GetAuthUserExam/{userExamId}")]
        public async Task<IActionResult> GetAuthUserExam(int userExamId)
        {
            var userAnswers = await userAnswerRepository.GetUserAnswersByUserExamId(userExamId);
            var userAnswersDto = mapper.Map<List<UserAnswerResponseDto>>(userAnswers);

            var examImplementationView = new List<ExamImplementationViewDto>();

            foreach (var userAnswer in userAnswersDto)
            {
                var questionOptions = mapper.Map<List<OptionResponseDto>>(await optionRepository.GetQuestionOptions(userAnswer.QuestionId));

                examImplementationView.Add(new ExamImplementationViewDto
                {
                    UserExamId = userAnswer.UserExamsId,
                    Question = userAnswer.Question,
                    OptionSelected = userAnswer.Option,
                    Options = questionOptions
                });
            }

            return Ok(examImplementationView);
        }

        [HttpGet]
        [Route("GetExamDetailsByUserExmId/{userExamId}")]
        public async Task<IActionResult> GetExamDetailsByUserExmId(int userExamId)
        {
            var examDetails = await userExamsRepository.GetByIdAsync(userExamId);
            return Ok(mapper.Map<ExamResponseDto>(examDetails.Exam));
        }

        [HttpDelete]
        [Route("DeleteUserExam/{userExamId}")]
        public async Task<IActionResult> DeleteUserExam(int userExamId)
        {
            await userExamsRepository.DeleteUserExam(userExamId);
            return NoContent();
        }


    }
}