﻿using Application.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PledgeRepo : GenericRepo<Pledge>, IPledgeRepo
    {
        private readonly ApiContext _context;

        public PledgeRepo(ApiContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Pledge> GetPledgeByIdAsync(int id)
        {
            return await _context.Pledges.FindAsync(id);
        }
        public async Task<Pledge> GetPledgeByUserIdAndProjectIdAsync(int userId, int projectId)
        {
            return await _context.Pledges
                .FirstOrDefaultAsync(p => p.UserId == userId && p.ProjectId == projectId);
        }
    }
}
