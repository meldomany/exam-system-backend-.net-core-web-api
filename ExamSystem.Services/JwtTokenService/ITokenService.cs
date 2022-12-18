using ExamSystem.Models;
using System.Collections.Generic;

namespace ExamSystem.Services.JwtTokenService
{
    public interface ITokenService
    {
        string CreateToken(string roleName, AppUser appUser);
    }
}
