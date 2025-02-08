using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IBoardRepo : IGenericRepo<Board>
    {
        public Task<List<Board>> GetBoardsByProjectId(int projectId);

    }
}
