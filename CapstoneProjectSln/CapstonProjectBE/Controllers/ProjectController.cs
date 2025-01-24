using Application.IService;
using Microsoft.AspNetCore.Mvc;


namespace CapstonProjectBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpGet("GetAllProject")]
        public async Task<IActionResult> GetAllProject()
        {
            return Ok(await _projectService.GetAllProjects());
        }

        [HttpGet("GetProjectsPaging")]
        public async Task<IActionResult> GetProjectsPaging(int pageNumber, int pageSize)
        {
            return Ok(await _projectService.GetProjectsPaging(pageNumber, pageSize));
        }

        [HttpGet("GetProjectById")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            return Ok(await _projectService.GetProjectById(id));
        }

        [HttpDelete("DeleteProject")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            return Ok(await _projectService.DeleteProject(id));
        }
    }
}
