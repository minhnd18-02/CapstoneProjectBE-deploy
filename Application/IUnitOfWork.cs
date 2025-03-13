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
        public IProjectRepo ProjectRepo { get; }
        public ICategoryRepo CategoryRepo { get; }
        public IRewardRepo RewardRepo { get; }
        public IGoalRepo GoalRepo { get; }
        public Task<int> SaveChangeAsync();
    }
}
