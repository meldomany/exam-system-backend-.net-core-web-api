using AutoMapper;
using ExamSystem.Models;
using ExamSystem.Models.DTOs.AppUser;
using ExamSystem.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(IMapper mapper, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        [Route("GetAuthUser")]
        public async Task<IActionResult> GetAuthUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToString();
            var user = await userManager.FindByIdAsync(userId);
            var appUserDto = mapper.Map<AppUserResponseDto>(user);
            return Ok(appUserDto);
        }

        [HttpPut]
        [Route("UpdateAuthUser")]
        public async Task<IActionResult> UpdateAuthUser(AppUserRequestDto appUserRequestDto)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToString());
                user.UserName = appUserRequestDto.Username;
                user.Email = appUserRequestDto.Email;
                user.FirstName = appUserRequestDto.FirstName;
                user.SurName = appUserRequestDto.SurName;
                user.JobTitle = appUserRequestDto.JobTitle;

                var checkUpdate = await userManager.UpdateAsync(user);
                if (checkUpdate.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest(checkUpdate.Errors);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userManager.Users.ToListAsync();
            var usersResponseDto = mapper.Map<List<AppUserResponseDto>>(users);
            foreach (var user in usersResponseDto)
            {
                var roles = await userManager.GetRolesAsync(mapper.Map<AppUser>(user));
                user.RoleName = roles.FirstOrDefault();
            }
            return Ok(usersResponseDto);
        }

        [HttpGet]
        [Route("GetUser/{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {

            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            var userRoles = await userManager.GetRolesAsync(user);

            var appUserResponseDto = mapper.Map<AppUserResponseDto>(user);
            appUserResponseDto.RoleName = userRoles.FirstOrDefault();

            return Ok(appUserResponseDto);
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(AppUserRequestDto appUserRequestDto)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByIdAsync(appUserRequestDto.Id);
                user.UserName = appUserRequestDto.Username;
                user.Email = appUserRequestDto.Email;
                user.FirstName = appUserRequestDto.FirstName;
                user.SurName = appUserRequestDto.SurName;
                user.JobTitle = appUserRequestDto.JobTitle;

                
                if(appUserRequestDto.RoleName == "Admin")
                {
                    await userManager.RemoveFromRoleAsync(user, WebConstants.TrainerRole);
                }
                else
                {
                    await userManager.RemoveFromRoleAsync(user, WebConstants.AdminRole);
                }

                await userManager.AddToRoleAsync(user, appUserRequestDto.RoleName);
                
                var checkUpdate = await userManager.UpdateAsync(user);
                if (checkUpdate.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest(checkUpdate.Errors);
                }
            }
            return BadRequest();
        }
    }
}
