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
        public IPledgeRepo PledgeRepo { get; }
        public ITokenRepo TokenRepo { get; }
        public IProjectRepo ProjectRepo { get; }
        public IPostRepo PostRepo { get; }
        public ICommentRepo CommentRepo { get; }
        public IPostCommentRepo PostCommentRepo { get; }
        public IProjectCommentRepo ProjectCommentRepo { get; }
        public Task<int> SaveChangeAsync();
    }
}
