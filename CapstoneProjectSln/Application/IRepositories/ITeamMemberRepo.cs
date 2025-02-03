using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface ITeamMemberRepo : IGenericRepo<TeamMember>
    {
        Task<TeamMember> GetByIdAsync(int teamId, int userId);
        Task<IEnumerable<TeamMember>> GetAllByTeamIdAsync(int teamId);
    }
}