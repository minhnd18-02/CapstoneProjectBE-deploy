using Application.ServiceResponse;
using Application.ViewModels.BoardDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IBoardService
    {
        public Task<ServiceResponse<int>> CreateBoard(CreateBoardDTO createBoardDTO);
        public Task<ServiceResponse<PaginationModel<BoardDTO>>> GetPaginatedBoardsByProjectId(int projectId, int page = 1, int pageSize = 20);
        public Task<ServiceResponse<List<BoardDTO>>> GetBoardsByProjectId(int projectId);
        public Task<ServiceResponse<string>> UpdateBoard(int boardId, CreateBoardDTO createBoardDTO);
        public Task<ServiceResponse<string>> RemoveBoard(int boardId);
    }
}
