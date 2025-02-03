using Application;
using Application.IRepositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services)
        {
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<ITokenRepo, TokenRepo>();
            services.AddScoped<ICardRepo, CardRepo>();
            services.AddScoped<IBoardRepo, BoardRepo>();
            services.AddScoped<IProjectRepo, ProjectRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
