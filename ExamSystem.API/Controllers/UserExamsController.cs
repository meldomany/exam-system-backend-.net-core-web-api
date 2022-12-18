using AutoMapper;
using ExamSystem.DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserExamsController : ControllerBase
    {
        private readonly IUserExamsRepository userExamsRepository;
        private readonly IMapper mapper;

        public UserExamsController(IUserExamsRepository userExamsRepository, IMapper mapper)
        {
            this.userExamsRepository = userExamsRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("GetUserExams")]
        public async Task<IActionResult> GetUserExams()
        {
            return Ok(await userExamsRepository.GetUserExamsAsync());
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
