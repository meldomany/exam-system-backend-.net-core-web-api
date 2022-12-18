using ExamSystem.DataAccess;
using ExamSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ExamSystem.API.Extentions
{
    public static class IdentityExtentions
    {
        public static IServiceCollection AddIdentityServicesExtention(this IServiceCollection services)
        {

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;

        }
    }
}
