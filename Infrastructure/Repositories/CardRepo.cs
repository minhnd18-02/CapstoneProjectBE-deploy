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
    public class CardRepo : GenericRepo<Card>, ICardRepo
    {
        private readonly ApiContext _dbContext;

        public CardRepo(ApiContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Card>> GetCardsByBoardId(int boardId)
        {
            return await _dbContext.Cards.Where(c => c.BoardId == boardId).ToListAsync();
        }
    }
}
