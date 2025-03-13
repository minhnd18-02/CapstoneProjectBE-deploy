using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface ICommentRepo : IGenericRepo<Comment>
    {
        public Task<List<Comment>> GetCommentsWithCommentsByPostId(int postId);
        public Task<List<Comment>> GetCommentsWithCommentsByProjectId(int projectId);
        public Task<List<Comment>> GetCommentsByUserId(int userId);
    }
}
