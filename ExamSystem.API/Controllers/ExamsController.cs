using AutoMapper;
using ExamSystem.DataAccess.IRepository;
using ExamSystem.Models;
using ExamSystem.Models.DTOs.Exam;
using ExamSystem.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamSystem.API.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamRepository examRepository;
        private readonly IMapper mapper;

        public ExamsController(IExamRepository examRepository, IMapper mapper)
        {
            this.examRepository = examRepository;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetExams")]
        public async Task<IActionResult> GetExams()
        {
            var exams = await examRepository.GetExamsAsync();
            var examsDto = mapper.Map<IEnumerable<ExamResponseDto>>(exams);
            return Ok(examsDto);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetExam/{id}")]
        public async Task<IActionResult> GetExam(int id)
        {
            var exam = await examRepository.GetByIdAsync(id);
            if (exam == null) return NotFound();
            var examDto = mapper.Map<ExamResponseDto>(exam);
            return Ok(examDto);
        }

        [HttpDelete]
        [Route("DeleteExam/{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            if (await examRepository.DeleteByIdAsync(id))
            {
                await examRepository.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [Route("CreateExam")]
        public async Task<IActionResult> CreateExam(ExamRequestDto examRequestDto)
        {
            if (ModelState.IsValid)
            {
                var examRequest = mapper.Map<Exam>(examRequestDto);
                examRequest.CreatedAt = DateTime.Now;
                if (await examRepository.CreateExamAsync(examRequest))
                {
                    await examRepository.SaveChangesAsync();
                    return StatusCode(201);
                }
                return BadRequest("This exam name is already exist");
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateExam")]
        public async Task<IActionResult> UpdateExam(ExamRequestDto examRequestDto)
        {
            if (ModelState.IsValid)
            {
                var examRequest = mapper.Map<Exam>(examRequestDto);
                if (await examRepository.UpdateExamAsync(examRequest))
                {
                    await examRepository.SaveChangesAsync();
                    return NoContent();
                }
                return BadRequest("This exam name is already exist");
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetFieldExams/{fieldId}")]
        public async Task<IActionResult> GetFieldExams(int fieldId)
        {
            var examsDto = mapper.Map<IEnumerable<ExamResponseDto>>(await examRepository.GetFieldExams(fieldId));
            return Ok(examsDto);
        }

        [HttpPost]
        [Route("CreateExams")]
        public async Task<IActionResult> CreateExam(List<ExamRequestDto> examsRequestDto)
        {
            if (ModelState.IsValid)
            {
                var examsRequest = mapper.Map<List<Exam>>(examsRequestDto);
                if (await examRepository.CreateExamsAsync(examsRequest))
                {
                    await examRepository.SaveChangesAsync();
                    return StatusCode(201);
                }
                return BadRequest("There is an exam name already exist");
            }
            return BadRequest();
        }
    }
}
