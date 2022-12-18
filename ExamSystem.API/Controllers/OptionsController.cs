using AutoMapper;
using OptionSystem.DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamSystem.Models.DTOs.Option;
using ExamSystem.Models;
using ExamSystem.DataAccess.Repository;
using ExamSystem.Shared;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace OptionSystem.API.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]
    [Route("api/[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly IOptionRepository optionRepository;
        private readonly IMapper mapper;

        public OptionsController(IOptionRepository optionRepository, IMapper mapper)
        {
            this.optionRepository = optionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("GetOptions")]
        public async Task<IActionResult> GetOptions()
        {
            var options = await optionRepository.GetOptionsAsync();
            var optionsDto = mapper.Map<IEnumerable<OptionResponseDto>>(options);
            return Ok(optionsDto);
        }

        [HttpGet]
        [Route("GetOption/{id}")]
        public async Task<IActionResult> GetOption(int id)
        {
            var option = await optionRepository.GetByIdAsync(id);
            if (option == null) return NotFound();
            var optionDto = mapper.Map<OptionResponseDto>(option);
            return Ok(optionDto);
        }

        [HttpDelete]
        [Route("DeleteOption/{id}")]
        public async Task<IActionResult> DeleteOption(int id)
        {
            if (await optionRepository.DeleteByIdAsync(id))
            {
                await optionRepository.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [Route("CreateOption")]
        public async Task<IActionResult> CreateOption(OptionRequestDto optionRequestDto)
        {
            if (ModelState.IsValid)
            {
                var optionRequest = mapper.Map<Option>(optionRequestDto);
                if (await optionRepository.CreateOptionAsync(optionRequest))
                {
                    await optionRepository.SaveChangesAsync();
                    return StatusCode(201);
                }
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateOption")]
        public async Task<IActionResult> UpdateOption(OptionRequestDto optionRequestDto)
        {
            if (ModelState.IsValid)
            {
                var optionRequest = mapper.Map<Option>(optionRequestDto);
                if (await optionRepository.UpdateOptionAsync(optionRequest))
                {
                    await optionRepository.SaveChangesAsync();
                    return NoContent();
                }
                return BadRequest();
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetQuestionOptions/{id}")]
        public async Task<IActionResult> GetQuestionOptions(int id)
        {
            var options = await optionRepository.GetQuestionOptions(id);
            var optionsDto = mapper.Map<IEnumerable<OptionResponseDto>>(options);
            return Ok(optionsDto);
        }

        [HttpPost]
        [Route("CreateOptions")]
        public async Task<IActionResult> CreateOptions(List<OptionRequestDto> optionsRequestDto)
        {
            if (ModelState.IsValid)
            {
                var optionsRequest = mapper.Map<List<Option>>(optionsRequestDto);
                if (await optionRepository.CreateOptionsAsync(optionsRequest))
                {
                    await optionRepository.SaveChangesAsync();
                    return StatusCode(201);
                }
                return BadRequest();
            }
            return BadRequest();
        }
    }
}
