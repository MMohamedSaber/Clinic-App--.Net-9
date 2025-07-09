using Clinic.API.Middleware;
using Clinic.API.Validator;
using Clinic.Infrastructure;
using FluentValidation;
namespace Clinic.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();
            // Add AutoMapper with all profiles in current assembly or specify assembly
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.infrastructureConfiguration(builder.Configuration);
            builder.Services.AddHttpContextAccessor();

            // Registers all validators (like UserValidator) found in the same assembly.
            // This scans the assembly for all classes that inherit from AbstractValidator<T> and registers them.
            builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<NurseValidator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //app.UseCors("AllowSwaggerUI");
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseMiddleware<GlobalHandlingExceptions>();
            app.Run();
        }
    }
}
