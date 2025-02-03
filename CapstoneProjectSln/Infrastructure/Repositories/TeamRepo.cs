using Application.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TeamRepo : GenericRepo<Team>, ITeamRepo
    {
        private readonly ApiContext _dbContext;
        public TeamRepo(ApiContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Team> GetByIdAsync(int id)
        {
            return await _dbContext.Teams
                .Include(t => t.TeamMembers)
                .FirstOrDefaultAsync(t => t.TeamId == id);
        }

        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _dbContext.Teams
                .Include(t => t.TeamMembers)
                .ToListAsync();
        }

        public async Task AddAsync(Team team)
        {
            await _dbContext.Teams.AddAsync(team);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Team team)
        {
            _dbContext.Teams.Update(team);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var team = await GetByIdAsync(id);
            if (team != null)
            {
                _dbContext.Teams.Remove(team);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
