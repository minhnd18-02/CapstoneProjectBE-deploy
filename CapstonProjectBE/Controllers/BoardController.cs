using Application.IService;
using Application.ViewModels.BoardDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapstonProjectBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;
        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBoard(CreateBoardDTO createBoardDTO)
        {
            var result = await _boardService.CreateBoard(createBoardDTO);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetBoardsByProjectId(int projectId)
        {
            var result = await _boardService.GetBoardsByProjectId(projectId);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet("pagination")]
        public async Task<IActionResult> GetPaginatedBoardsByProjectId(int projectId, int page = 1, int pageSize = 20)
        {
            var result = await _boardService.GetPaginatedBoardsByProjectId(projectId, page, pageSize);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateBoard(int boardId, CreateBoardDTO createBoardDTO)
        {
            var result = await _boardService.UpdateBoard(boardId, createBoardDTO);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> RemoveBoard(int boardId)
        {
            var result = await _boardService.RemoveBoard(boardId);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
