using Application.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BoardRepo : GenericRepo<Board>, IBoardRepo
    {
        private readonly ApiContext _dbContext;

        public BoardRepo(ApiContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Board>> GetBoardsByProjectId(int projectId)
        {
            return await _dbContext.Boards.Where(b => b.ProjectId == projectId).ToListAsync();
        }
    }
}
