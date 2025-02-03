using Application.IRepositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProjectRepo : GenericRepo<Project>, IProjectRepo
    {
        public ProjectRepo(ApiContext context) : base(context)
        {
        }
    }
}
