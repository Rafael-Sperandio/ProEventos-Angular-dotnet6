using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProEventos.API.Data;

namespace ProEventos.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            var connection = builder.Configuration.GetConnectionString("SqlLiteConnection");
            // Add services to the container.
            builder.Services.AddDbContext<DataContext>(
                context => context.UseSqlite(connection)
                );
            builder.Services.AddControllers();
            builder.Services.AddCors();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pokedex.API", Version = "v1" });
            });

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProEventos.API v1"));
            }
            //https://localhost:5001/swagger/index.html

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(cors => cors.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());

            app.MapControllers();

            app.Run();
        }
    }
}
