using Application.ServiceResponse;
using Application.ViewModels.TeamMemberDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface ITeamMemberService
    {
        Task<ServiceResponse<TeamMemberDTO>> GetByIdAsync(int teamId, int userId);
        Task<ServiceResponse<IEnumerable<TeamMemberDTO>>> GetAllByTeamIdAsync(int teamId);
        Task<ServiceResponse<TeamMemberDTO>> AddAsync(TeamMemberDTO teamMemberDTO);
        Task<ServiceResponse<TeamMemberDTO>> UpdateAsync(TeamMemberDTO teamMemberDTO);
        Task<ServiceResponse<bool>> DeleteAsync(int teamId, int userId);
    }
}

