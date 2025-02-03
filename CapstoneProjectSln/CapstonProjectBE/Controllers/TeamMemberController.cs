using Application.IService;
using Application.ServiceResponse;
using Application.ViewModels.TeamMemberDTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstonProjectBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private readonly ITeamMemberService _teamMemberService;

        public TeamMemberController(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
        }

        [HttpGet("{teamId}/{userId}")]
        public async Task<ActionResult<ServiceResponse<TeamMemberDTO>>> GetByTeamIdAndUserId([FromRoute] int teamId, [FromRoute] int userId)
        {
            var response = await _teamMemberService.GetByTeamIdAndUserId(teamId, userId);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("team/{teamId}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<TeamMemberDTO>>>> GetAllByTeamId([FromRoute] int teamId)
        {
            var response = await _teamMemberService.GetAllByTeamIdAsync(teamId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<TeamMemberDTO>>> Add(TeamMemberDTO teamMemberDTO)
        {
            var response = await _teamMemberService.AddAsync(teamMemberDTO);
            if (!response.Success || response.Data == null)
            {
                return BadRequest(response);
            }
            return CreatedAtAction(nameof(GetByTeamIdAndUserId), new { teamId = response.Data.TeamId, userId = response.Data.UserId }, response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<TeamMemberDTO>>> Update(TeamMemberDTO teamMemberDTO)
        {
            var response = await _teamMemberService.UpdateAsync(teamMemberDTO);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        //[HttpDelete("{teamId}/{userId}")]
        //public async Task<ActionResult<ServiceResponse<bool>>> Delete(int teamId, int userId)
        //{
        //    var response = await _teamMemberService.DeleteAsync(teamId, userId);
        //    if (!response.Success)
        //    {
        //        return NotFound(response);
        //    }
        //    return Ok(response);
        //}
    }
}

