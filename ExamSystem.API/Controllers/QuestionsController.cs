using AutoMapper;
using QuestionSystem.DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamSystem.Models.DTOs.Question;
using ExamSystem.Models;
using ExamSystem.DataAccess.Repository;
using ExamSystem.Shared;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace QuestionSystem.API.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository questionRepository;
        private readonly IMapper mapper;

        public QuestionsController(IQuestionRepository questionRepository, IMapper mapper)
        {
            this.questionRepository = questionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("GetQuestions")]
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await questionRepository.GetQuestionsAsync();
            var questionsDto = mapper.Map<IEnumerable<QuestionResponseDto>>(questions);
            return Ok(questionsDto);
        }

        [HttpGet]
        [Route("GetQuestion/{id}")]
        public async Task<IActionResult> GetQuestion(int id)
        {
            var question = await questionRepository.GetByIdAsync(id);
            if (question == null) return NotFound();
            var questionDto = mapper.Map<QuestionResponseDto>(question);
            return Ok(questionDto);
        }

        [HttpDelete]
        [Route("DeleteQuestion/{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            if (await questionRepository.DeleteByIdAsync(id))
            {
                await questionRepository.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }

        [HttpPost]
        [Route("CreateQuestion")]
        public async Task<IActionResult> CreateQuestion(QuestionRequestDto questionRequestDto)
        {
            if (ModelState.IsValid)
            {
                var questionRequest = mapper.Map<Question>(questionRequestDto);
                if (await questionRepository.CreateQuestionAsync(questionRequest))
                {
                    await questionRepository.SaveChangesAsync();
                    return StatusCode(201);
                }
                return BadRequest("This question name is already exist");
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateQuestion")]
        public async Task<IActionResult> UpdateQuestion(QuestionRequestDto questionRequestDto)
        {
            if (ModelState.IsValid)
            {
                var questionRequest = mapper.Map<Question>(questionRequestDto);
                if (await questionRepository.UpdateQuestionAsync(questionRequest))
                {
                    await questionRepository.SaveChangesAsync();
                    return NoContent();
                }
                return BadRequest("This question name is already exist");
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetExamQuestions/{id}")]
        public async Task<IActionResult> GetExamQuestions(int id)
        {
            var questions = await questionRepository.GetExamQuestions(id);
            var questionsDto = mapper.Map<IEnumerable<QuestionResponseDto>>(questions);
            return Ok(questionsDto);
        }

        [HttpPost]
        [Route("CreateQuestions")]
        public async Task<IActionResult> CreateQuestions(List<QuestionRequestDto> questionsRequestDto)
        {
            if (ModelState.IsValid)
            {
                var questionsRequest = mapper.Map<List<Question>>(questionsRequestDto);
                if (await questionRepository.CreateQuestionsAsync(questionsRequest))
                {
                    await questionRepository.SaveChangesAsync();
                    return StatusCode(201);
                }
                return BadRequest("There is a question name already exist");
            }
            return BadRequest();
        }

    }
}
