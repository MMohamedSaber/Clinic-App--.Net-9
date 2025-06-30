
using Microsoft.Data.SqlClient;
using Clinic.Infrastructure;
using Clinic.API.Middleware;
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
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowSwaggerUI", policy =>
            //    {
            //        policy.WithOrigins("https://localhost:7137") // Õÿ —«»ÿ Swagger Â‰«
            //              .AllowAnyMethod()
            //              .AllowAnyHeader();
            //    });
            //});
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
