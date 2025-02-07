using Application.IService;
using Application.ViewModels.CardDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapstonProjectBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCard(CreateCardDTO createCardDTO)
        {
            var result = await _cardService.CreateCard(createCardDTO);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCardsByBoardId(int boardId)
        {
            var result = await _cardService.GetCardsByBoardId(boardId);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet("pagination")]
        public async Task<IActionResult> GetPaginatedCardsByBoardId(int boardId, int page = 1, int pageSize = 20)
        {
            var result = await _cardService.GetPaginatedCardsByBoardId(boardId, page, pageSize);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateCard(int cardId, CreateCardDTO createCardDTO)
        {
            var result = await _cardService.UpdateCard(cardId, createCardDTO);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> RemoveCard(int cardId)
        {
            var result = await _cardService.RemoveCard(cardId);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
