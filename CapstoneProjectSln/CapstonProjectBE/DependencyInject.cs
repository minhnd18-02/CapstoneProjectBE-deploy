using Application.IRepositories;
using Application.IService;
using Application.Services;
using Infrastructure;

namespace CapstonProjectBE
{
    public static class DependencyInject
    {
        public static IServiceCollection AddWebAPIService(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.PropertyNamingPolicy = new KebabCaseNamingPolicy();
            });
            services.AddScoped<IAuthenService, AuthenService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ITeamMemberService, TeamMemberService>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHealthChecks();
            return services;
        }
    }
}
