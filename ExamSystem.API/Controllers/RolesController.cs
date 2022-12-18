using ExamSystem.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSystem.API.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            if (roles.Count > 0) return Ok(roles);
            return Ok("There are no roles yet");
        }

        [HttpPost]
        [Route("AddRoles/{roleName}")]
        public async Task<IActionResult> AddRoles(string roleName)
        {
            if (roleName == null) return BadRequest("You should pass the role name");

            var roleCheck = await _roleManager.FindByNameAsync(roleName);
            if (roleCheck != null) return BadRequest("Role name is already exist");
            
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
                return Ok(result);
            if (result.Errors.Count() > 0)
                return BadRequest(result.Errors);

            return BadRequest("Failed to create a new role");
        }

        [HttpDelete]
        [Route("DeleteRoles/{id}")]
        public async Task<IActionResult> DeleteRoles(string id)
        {
            if (id == null) return BadRequest("You should pass the role id");
            
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return BadRequest("There is no role with this id");

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return NoContent();
            if (result.Errors.Count() > 0)
                return BadRequest(result.Errors);

            return BadRequest("Failed to delete the role");
        }

        [HttpPut]
        [Route("UpdateRoles")]
        public async Task<IActionResult> UpdateRoles(IdentityRole role)
        {
            if (role == null) return BadRequest("You should pass a role to update it");

            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
                return NoContent();
            if (result.Errors.Count() > 0)
                return BadRequest(result.Errors);

            return BadRequest("Failed to update the role");
        }
    }
}
