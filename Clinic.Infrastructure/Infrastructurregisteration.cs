
using Clinic.Core.Entities;
using Clinic.Core.Interfaces;
using Clinic.Core.Interfaces.Services;
using Clinic.Infrastructure.Data;
using Clinic.Infrastructure.Repositories;
using Clinic.Infrastructure.Repositories.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Infrastructure
{
    public static  class Infrastructurregisteration
    {
        public static IServiceCollection infrastructureConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            // apply dbContext 
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGenerateToken, GenerateToken>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer
                            (configuration.GetConnectionString("Default")));

            //add Identity User
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

            

            //apply unit of work

            return services;

        }
    }
}
