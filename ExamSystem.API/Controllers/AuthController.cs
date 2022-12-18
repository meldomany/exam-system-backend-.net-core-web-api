using AutoMapper;
using ExamSystem.Models;
using ExamSystem.Models.DTOs.Auth;
using ExamSystem.Services.JwtTokenService;
using ExamSystem.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.roleManager = roleManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                
                if (user == null) return BadRequest("Check your email address");

                string roleName = WebConstants.TrainerRole;
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains(WebConstants.AdminRole))
                {
                    roleName = WebConstants.AdminRole;
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.PasswordHash, false);
                
                if (result.Succeeded)
                {
                    var token = _tokenService.CreateToken(roleName, user);
                    var authResponse = new AuthResponse()
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Email = user.Email,
                        RoleName = roleName,
                        Token = token
                    };
                    return Ok(authResponse);
                }
                else
                {
                    return BadRequest("Check your credintials");
                }
            }
            return BadRequest(ModelState.Values);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(registerDto.Email);

                if (user != null) return BadRequest("Email address already in use");

                var appUser = _mapper.Map<AppUser>(registerDto);

                var newUser = await _userManager.CreateAsync(appUser, registerDto.PasswordHash);
                
                if (newUser.Errors.Count() > 0)
                {
                    return BadRequest(newUser.Errors);
                }

                if (newUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, WebConstants.TrainerRole);

                    var loginDto = new LoginDto
                    {
                        Email = registerDto.Email,
                        PasswordHash = registerDto.PasswordHash
                    };

                    return await Login(loginDto);
                }

                return BadRequest();
            }
            return BadRequest(ModelState.Values);
        }


        [HttpPost]
        [Route("CheckDatabaseRolesAvailable")]
        public async Task<bool> CheckDatabaseRolesAvailable()
        {
            var rolesDb = await roleManager.Roles.ToListAsync();
            // check if there are no roles available in db
            if (rolesDb.Count == 0)
            {
                // deleting all users in db if there
                var users = await _userManager.Users.ToListAsync();
                if(users.Count > 0)
                {
                    foreach (var user in users)
                    {
                        await _userManager.DeleteAsync(user);
                    }
                }

                // create two default roles "admin - trainer"
                var roles = new List<IdentityRole>
                {
                    new IdentityRole(){ Name = WebConstants.AdminRole },
                    new IdentityRole(){ Name = WebConstants.TrainerRole }
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }

                // create two default users and assign roles
                var appUsers = new List<AppUser>
                {
                    new AppUser { UserName = WebConstants.AdminRole, Email = "admin@gmail.com"},
                    new AppUser { UserName = WebConstants.TrainerRole, Email = "trainer@gmail.com"},
                };

                foreach (var appUser in appUsers)
                {
                    await _userManager.CreateAsync(appUser, "123abcABC@");

                    await _userManager.AddToRoleAsync(appUser, appUser.UserName);
                }
                return true;
            }
            return false;
        }
    }
}
