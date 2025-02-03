using Application.ServiceResponse;
using Application.ViewModels.CardDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface ICardService
    {
        public Task<ServiceResponse<int>> CreateCard(CreateCardDTO createCardDTO);
        public Task<ServiceResponse<PaginationModel<CardDTO>>> GetPaginatedCardsByBoardId(int boardId, int page = 1, int pageSize = 20);
        public Task<ServiceResponse<List<CardDTO>>> GetCardsByBoardId(int boardId);
        public Task<ServiceResponse<string>> UpdateCard(int cardId, CreateCardDTO createCardDTO);
        public Task<ServiceResponse<string>> RemoveCard(int cardId);

    }
}
