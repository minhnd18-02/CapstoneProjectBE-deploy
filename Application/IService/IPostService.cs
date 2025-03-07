using Application.ServiceResponse;
using Application.ViewModels.PostDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IPostService
    {
        public Task<ServiceResponse<int>> CreatePost(CreatePostDTO createCardDTO);
        public Task<ServiceResponse<PaginationModel<PostDTO>>> GetPaginatedPostsByUserId(int userId, int page = 1, int pageSize = 20);
        public Task<ServiceResponse<List<PostDTO>>> GetPostsByUserId(int userId);
        public Task<ServiceResponse<string>> UpdatePost(int postId, CreatePostDTO updatePostDTO);
        public Task<ServiceResponse<string>> RemovePost(int postId);
    }
}
