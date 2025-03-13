using Application;
using Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiContext _apiContext;
        private readonly IUserRepo _userRepository;
        private readonly ITokenRepo _tokenRepo;
        private readonly IProjectRepo _projectRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IRewardRepo _rewardRepo;
        private readonly IGoalRepo _goalRepo;

        public UnitOfWork(ApiContext apiContext, IUserRepo userRepository, ITokenRepo tokenRepo, IProjectRepo projectRepo, ICategoryRepo categoryRepo,
            IRewardRepo rewardRepo, IGoalRepo goalRepo)
        {
            _apiContext = apiContext;
            _tokenRepo = tokenRepo;
            _userRepository = userRepository;
            _projectRepo = projectRepo;
            _categoryRepo = categoryRepo;
            _rewardRepo = rewardRepo;
            _goalRepo = goalRepo;
        }

        public IUserRepo UserRepository => _userRepository;
        public ITokenRepo TokenRepo => _tokenRepo;
        public IProjectRepo ProjectRepo => _projectRepo;
        public ICategoryRepo CategoryRepo => _categoryRepo;
        public IRewardRepo RewardRepo => _rewardRepo;
        public IGoalRepo GoalRepo => _goalRepo;


        public async Task<int> SaveChangeAsync()
        {
            try
            {
                return await _apiContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log exception details here
                throw new ApplicationException("An error occurred while saving changes.", ex);
            }
        }
    }
}
