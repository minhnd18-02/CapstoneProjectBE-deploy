using Application.ServiceResponse;
using Application.ViewModels.TeamDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface ITeamService
    {
        Task<ServiceResponse<TeamDTO>> GetByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<TeamDTO>>> GetAllAsync();
        Task<ServiceResponse<TeamDTO>> CreateTeamAsync(TeamDTO teamDTO);
        Task<ServiceResponse<TeamDTO>> UpdateAsync(int id, TeamDTO teamDTO);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
