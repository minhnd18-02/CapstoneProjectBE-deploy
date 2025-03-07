using Application.IService;
using Application.ViewModels.CommentDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CapstonProjectBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePostComment(CreatePostCommentDTO createPostCommentDTO)
        {
            var result = await _commentService.CreatePostComment(createPostCommentDTO);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentsByPostId(int postId)
        {
            var result = await _commentService.GetCommentsByPostId(postId);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetPaginatedCommentsByPostId(int postId, int page = 1, int pageSize = 20)
        {
            var result = await _commentService.GetPaginatedCommentsByPostId(postId, page, pageSize);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetCommentsByProjectId(int projectId)
        {
            var result = await _commentService.GetCommentsByProjectId(projectId);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetPaginatedCommentsByProjectId(int projectId, int page = 1, int pageSize = 20)
        {
            var result = await _commentService.GetPaginatedCommentsByProjectId(projectId, page, pageSize);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetCommentsByUserId(int userId)
        {
            var result = await _commentService.GetCommentsByUserId(userId);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetPaginatedCommentsByUserId(int userId, int page = 1, int pageSize = 20)
        {
            var result = await _commentService.GetPaginatedCommentsByUserId(userId, page, pageSize);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateComment(int commentId, UpdateCommentDTO updateCommentDTO)
        {
            var result = await _commentService.UpdateComment(commentId, updateCommentDTO);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> RemoveComment(int commentId)
        {
            var result = await _commentService.RemoveComment(commentId);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> SoftRemoveComment(int commentId)
        {
            var result = await _commentService.SoftRemoveComment(commentId);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
