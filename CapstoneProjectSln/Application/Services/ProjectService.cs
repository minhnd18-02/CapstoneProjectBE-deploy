using Application.IService;
using Application.ViewModels.Other;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> DeleteProject(int id)
        {
            return await _unitOfWork.ProjectRepo.DeleteProject(id);
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _unitOfWork.ProjectRepo.GetAll();
        }

        public async Task<Project> GetProjectById(int id)
        {
            return await _unitOfWork.ProjectRepo.GetProjectById(id);
        }

        public async Task<PagingResponse> GetProjectsPaging(int pageNumber, int pageSize)
        {
           var projects = await _unitOfWork.ProjectRepo.GetProjectsPaging(pageNumber, pageSize);
            var pagingResponse = new PagingResponse
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecord = projects.Item1,
                TotalPage = projects.Item2,
                Data = projects.Item3
            };
            return pagingResponse;
        }
    }
}
