using Application.ViewModels.Other;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IProjectService
    {
        public Task<IEnumerable<Project>> GetAllProjects();
        public Task<PagingResponse> GetProjectsPaging(int pageNumber, int pageSize);
        public Task<Project> GetProjectById(int id);
        public Task<int> DeleteProject(int id);
    }
}   
