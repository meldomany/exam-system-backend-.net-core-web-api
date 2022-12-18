using ExamSystem.DataAccess.IRepository;
using ExamSystem.Models.DTOs.ExamCreation;
using ExamSystem.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExamSystem.API.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]
    [Route("api/[controller]")]
    [ApiController]
    public class ExamCreationController : ControllerBase
    {
        private readonly IExamCreation examCreation;

        public ExamCreationController(IExamCreation examCreation)
        {
            this.examCreation = examCreation;
        }

        [HttpPost]
        [Route("ExamCreationPost")]
        public async Task<IActionResult> ExamCreationPost(ExamCreationRequestDto examCreationRequestDto)
        {
            if (ModelState.IsValid)
            {
                var createMessage = await examCreation.ExamCreationAsync(examCreationRequestDto);
                if (createMessage == "created")
                {
                    return StatusCode(201);
                }
                return BadRequest(createMessage);
            }
            return BadRequest("Check your data");
        }
    }
}
