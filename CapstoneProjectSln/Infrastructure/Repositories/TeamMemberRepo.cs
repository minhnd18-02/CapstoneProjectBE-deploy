﻿using Application.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TeamMemberRepo : GenericRepo<TeamMember>, ITeamMemberRepo
    {
        private readonly ApiContext _dbContext;

        public TeamMemberRepo(ApiContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TeamMember> GetByIdAsync(int teamId, int userId)
        {
            return await _dbContext.TeamMembers
                .Include(tm => tm.Team)
                .Include(tm => tm.User)
                .FirstOrDefaultAsync(tm => tm.TeamId == teamId && tm.UserId == userId);
        }

        public async Task<IEnumerable<TeamMember>> GetAllByTeamIdAsync(int teamId)
        {
            return await _dbContext.TeamMembers
                .Include(tm => tm.Team)
                .Include(tm => tm.User)
                .Where(tm => tm.TeamId == teamId)
                .ToListAsync();
        }
    }
}