using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface ITeamMemberRepo : IGenericRepo<TeamMember>
    {
        Task<TeamMember> GetByIdAsync(int teamId);
        Task<IEnumerable<TeamMember>> GetAllByTeamIdAsync(int teamId);
        Task UpdateAsync(TeamMember teamMember);
    }
}