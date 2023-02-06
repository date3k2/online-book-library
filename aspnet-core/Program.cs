using BookProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BookProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCors();
            builder.Services.AddSwaggerGen();
            string connectionString = builder.Configuration.GetConnectionString("BooksProject")!;
            builder.Services.AddDbContext<BooksContext>(options => options.UseSqlServer(connectionString).UseLazyLoadingProxies());
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}