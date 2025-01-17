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
            return services;
        }
    }
}
