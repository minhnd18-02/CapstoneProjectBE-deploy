using System.Reflection;
using Application.Commons;
using Infrastructure;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Application;
using System.IO;
using System.Linq;
using System;

namespace CapstonProjectBE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Add services to the container.
            var configuration = builder.Configuration;
            var myConfig = new AppConfiguration();
            configuration.Bind(myConfig);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApiContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddSingleton(myConfig);
            builder.Services.AddInfrastructuresService();
            builder.Services.AddWebAPIService();

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperConfigurationsProfile>();
            });

            var mapper = mapperConfiguration.CreateMapper();
            builder.Services.AddSingleton(mapper);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
