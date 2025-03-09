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
        private readonly IPostRepo _postRepo;
        private readonly ICommentRepo _commentRepo;
        private readonly IPostCommentRepo _postCommentRepo;
        private readonly IProjectCommentRepo _projectCommentRepo;
        public UnitOfWork(ApiContext apiContext, IUserRepo userRepository, ITokenRepo tokenRepo, IProjectRepo projectRepo, IPostRepo postRepo, ICommentRepo commentRepo, IPostCommentRepo postCommentRepo, IProjectCommentRepo projectCommentRepo, IPledgeRepo pledgeRepo)
        {
            _apiContext = apiContext;
            _tokenRepo = tokenRepo;
            _userRepository = userRepository;
            _projectRepo = projectRepo;
            _postRepo = postRepo;
            _commentRepo = commentRepo;
            _postCommentRepo = postCommentRepo;
            _projectCommentRepo = projectCommentRepo;
            _pledgeRepo = pledgeRepo;
        }

        public IUserRepo UserRepository => _userRepository;

        public ITokenRepo TokenRepo => _tokenRepo;

        public IPledgeRepo PledgeRepo => _pledgeRepo;

        public IProjectRepo ProjectRepo => _projectRepo;

        public IPostRepo PostRepo => _postRepo;

        public ICommentRepo CommentRepo => _commentRepo;
        
        public IPostCommentRepo PostCommentRepo => _postCommentRepo;
        
        public IProjectCommentRepo ProjectCommentRepo => _projectCommentRepo;

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
