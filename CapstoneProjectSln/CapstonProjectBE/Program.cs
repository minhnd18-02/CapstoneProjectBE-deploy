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
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("Staff", policy => policy.RequireRole("Staff"));
                options.AddPolicy("Customer", policy => policy.RequireRole("Customer"));
            });
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddInfrastructuresService();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                IConfiguration config = builder.Configuration; // Correct way to access the configuration
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = config["JWTSection:Issuer"],
                    ValidAudience = config["JWTSection:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTSection:SecretKey"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        var exception = context.Exception;
                        Console.WriteLine("Token validation failed: " + exception.Message);
                        return Task.CompletedTask;
                    }
                };
            })
            .AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");
                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["ClientSecret"];
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "GameMkt.API",
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. " +
                                  "\n\nEnter your token in the text input below. " +
                                  "\n\nExample: '12345abcde'",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            //var app = builder.Build();

            //// Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            #region
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8081";
            builder.WebHost.UseUrls($"http://*:{port}");

            var app = builder.Build();

            //Get swagger.json following root directory 
            app.UseSwagger(options => { options.RouteTemplate = "{documentName}/swagger.json"; });
            //Load swagger.json following root directory 
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/v1/swagger.json", "GameMkt.API V1"); c.RoutePrefix = string.Empty; });
            #endregion
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
