using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface ITeamMemberRepo : IGenericRepo<TeamMember>
    {
        Task<TeamMember?> GetByTeamIdAndUserIdIcludingTeamAndUserAsync(int teamId, int userId);
        Task<IEnumerable<TeamMember>> GetAllByTeamIdAsync(int teamId);
        Task UpdateAsync(TeamMember teamMember);
    }
}