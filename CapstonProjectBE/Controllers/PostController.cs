using Application.IService;
using Application.ViewModels.PostDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CapstonProjectBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PostController : Controller
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostDTO createPostDTO)
        {
            var result = await _postService.CreatePost(createPostDTO);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPostsByUserId(int userId)
        {
            var result = await _postService.GetPostsByUserId(userId);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet("pagination")]
        public async Task<IActionResult> GetPaginatedPostsByUserId(int userId, int page = 1, int pageSize = 20)
        {
            var result = await _postService.GetPaginatedPostsByUserId(userId, page, pageSize);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdatePost(int postId, CreatePostDTO createPostDTO)
        {
            var result = await _postService.UpdatePost(postId, createPostDTO);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> RemovePost(int postId)
        {
            var result = await _postService.RemovePost(postId);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
