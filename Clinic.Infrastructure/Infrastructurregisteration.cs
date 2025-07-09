
using System.Text;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces;
using Clinic.Core.Interfaces.Services;
using Clinic.Infrastructure.Data;
using Clinic.Infrastructure.Repositories;
using Clinic.Infrastructure.Repositories.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace Clinic.Infrastructure
{
    public static class Infrastructurregisteration
    {
        public static IServiceCollection infrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
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



            // Enables automatic model validation using FluentValidation.
            // This means any model that has a validator will be automatically validated before hitting the controller.
            services.AddFluentValidationAutoValidation();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
  .AddJwtBearer(options =>
  {
      options.Events = new JwtBearerEvents
      {
          OnMessageReceived = context =>
          {
              var token = context.Request.Cookies["token"];
              if (!string.IsNullOrEmpty(token))
              {
                  context.Token = token;
              }
              return Task.CompletedTask;
          }
      };

      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidateAudience = false,
          ValidateIssuerSigningKey = true,
          ValidIssuer = configuration["Token:Issuer"],
          IssuerSigningKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(configuration["Token:Secret"]))
      };
  });
            return services;
        }
    }
}
