using Application.ServiceResponse;
using Application.ViewModels.ProjectDTO;
using Domain.Entities;

namespace Application.IService
{
    public interface IProjectService
    {
        public Task<ServiceResponse<IEnumerable<Project>>> GetAllProjects();
        public Task<ServiceResponse<PaginationModel<Project>>> GetProjectsPaging(int pageNumber, int pageSize);
        public Task<ServiceResponse<Project>> GetProjectById(int id);
        public Task<ServiceResponse<int>> DeleteProject(int id);
        public Task<ServiceResponse<CreateProjectDto>> CreateProject(CreateProjectDto createProjectDto);
        public Task<ServiceResponse<UpdateProjectDto>> UpdateProject(int projectId, UpdateProjectDto updateProjectDto);
    }
}   
