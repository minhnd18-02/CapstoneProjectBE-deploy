using Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUnitOfWork
    {
        public IUserRepo UserRepository { get; }
        public ITokenRepo TokenRepo { get; }
        public ICardRepo CardRepo { get; }
        public IBoardRepo BoardRepo { get; }
        public Task<int> SaveChangeAsync();
    }
}
