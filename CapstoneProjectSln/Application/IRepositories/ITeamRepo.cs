using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface ITeamRepo : IGenericRepo<Team>
    {
        Task<Team?> GetByIdIncludingTeamMemberAsync(int id);
        Task<IEnumerable<Team>> GetAllIcludingTeamMembersAsync();
        Task UpdateAsync(Team team);
        Task DeleteAsync(int id);
    }
}
