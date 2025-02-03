using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IProjectRepo : IGenericRepo<Project>
    {
        Task<IEnumerable<Project>> GetAll();
        Task<(int, int, IEnumerable<Project>)> GetProjectsPaging(int pageNumber, int pageSize);
        Task<Project> GetProjectById(int id);
        Task<int> DeleteProject(int id);
        Task<int> UpdateProject(int id, Project project);   
        Task<Project> CreateProject(Project project);
    }
}
