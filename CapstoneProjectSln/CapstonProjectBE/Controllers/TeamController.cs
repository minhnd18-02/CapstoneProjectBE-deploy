using Application.IService;
using Application.ServiceResponse;
using Application.ViewModels.TeamDTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstonProjectBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<TeamDTO>>> GetById(int id)
        {
            var response = await _teamService.GetByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<TeamDTO>>>> GetAll()
        {
            var response = await _teamService.GetAllAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<TeamDTO>>> CreateTeam(TeamDTO teamDTO)
        {
            var response = await _teamService.CreateTeamAsync(teamDTO);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Name }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<TeamDTO>>> UpdateTeam(int id, TeamDTO teamDTO)
        {
            var response = await _teamService.UpdateAsync(id, teamDTO);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<ServiceResponse<bool>>> DeleteTeam(int id)
        //{
        //    var response = await _teamService.DeleteAsync(id);
        //    if (!response.Success)
        //    {
        //        return NotFound(response);
        //    }
        //    return Ok(response);
        //}
    }
}
