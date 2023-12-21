using System;
using Microsoft.EntityFrameworkCore;
using ReposetoryPatternWith_UOW.Core;
using ReposetoryPatternWith_UOW.Core.Interfaces;
using ReposetoryPatternWith_UOW.EF;
using ReposetoryPatternWith_UOW.EF.Repositories;
namespace ReposetoryPatternWith_UOW.Api
 
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDBContext>(Options =>
            Options.UseSqlServer(
                builder.Configuration.GetConnectionString("cs"),
                b => b.MigrationsAssembly("ReposetoryPatternWith_UOW.Api"))
            );

            //builder.Services.AddTransient( typeof(IBaseRepository<>),typeof(BaseRepository<>));
            builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
