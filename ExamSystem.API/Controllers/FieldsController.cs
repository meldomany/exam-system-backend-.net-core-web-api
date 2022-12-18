using AutoMapper;
using ExamSystem.DataAccess.IRepository;
using ExamSystem.Models;
using ExamSystem.Models.DTOs.Field;
using ExamSystem.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamSystem.API.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]
    [Route("api/[controller]")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private readonly IFieldRepository fieldRepository;
        private readonly IMapper mapper;

        public FieldsController(IFieldRepository fieldRepository, IMapper mapper)
        {
            this.fieldRepository = fieldRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("GetFields")]
        public async Task<IActionResult> GetFields()
        {
            var fields = await fieldRepository.GetFieldsAsync();
            var fieldsDto = mapper.Map<IEnumerable<FieldResponseDto>>(fields);
            return Ok(fieldsDto);
        }

        [HttpGet]
        [Route("GetField/{id}")]
        public async Task<IActionResult> GetField(int id)
        {
            var field = await fieldRepository.GetByIdAsync(id);
            if (field == null) return NotFound();
            var fieldDto = mapper.Map<FieldResponseDto>(field);
            return Ok(fieldDto);
        }

        [HttpDelete]
        [Route("DeleteField/{id}")]
        public async Task<IActionResult> DeleteField(int id)
        {
            if (await fieldRepository.DeleteByIdAsync(id))
            {
                await fieldRepository.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [Route("CreateField")]
        public async Task<IActionResult> CreateField(FieldRequestDto fieldRequestDto)
        {
            if (ModelState.IsValid)
            {
                var fieldRequest = mapper.Map<Field>(fieldRequestDto);
                if (await fieldRepository.CreateFieldAsync(fieldRequest))
                {
                    await fieldRepository.SaveChangesAsync();
                    return StatusCode(201);
                }
                return BadRequest("This field name is already exist");
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateField")]
        public async Task<IActionResult> UpdateField(FieldRequestDto fieldRequestDto)
        {
            if (ModelState.IsValid)
            {
                var fieldRequest = mapper.Map<Field>(fieldRequestDto);
                if (await fieldRepository.UpdateFieldAsync(fieldRequest))
                {
                    await fieldRepository.SaveChangesAsync();
                    return NoContent();
                }
                return BadRequest("This field name is already exist");
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("CreateFields")]
        public async Task<IActionResult> CreateFields(List<FieldRequestDto> fieldsRequestDto)
        {
            if (ModelState.IsValid)
            {
                var fieldsRequest = mapper.Map<List<Field>>(fieldsRequestDto);
                if (await fieldRepository.CreateFieldsAsync(fieldsRequest))
                {
                    await fieldRepository.SaveChangesAsync();
                    return StatusCode(201);
                }
                return BadRequest("There is a field name is already exist");
            }
            return BadRequest();
        }
    }
}