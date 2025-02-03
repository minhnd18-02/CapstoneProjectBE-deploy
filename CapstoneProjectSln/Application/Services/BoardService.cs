using Application.IService;
using Application.ServiceResponse;
using Application.Utils;
using Application.ViewModels.BoardDTO;
using Application.ViewModels.CardDTO;
using AutoMapper;
using CloudinaryDotNet.Actions;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BoardService : IBoardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BoardService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<int>> CreateBoard(CreateBoardDTO createBoardDTO)
        {
            var response = new ServiceResponse<int>();

            try
            {
                var validationContext = new ValidationContext(createBoardDTO);
                var validationResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(createBoardDTO, validationContext, validationResults, true))
                {
                    var errorMessages = validationResults.Select(r => r.ErrorMessage);
                    response.Success = false;
                    response.Message = string.Join("; ", errorMessages);
                    return response;
                }

                var existingProject = await _unitOfWork.ProjectRepo.GetByIdAsync(createBoardDTO.ProjectId);
                if (existingProject == null)
                {
                    response.Success = false;
                    response.Message = "Project not found";
                    return response;
                }

                Board board = new Board();
                board.Label = createBoardDTO.Label;
                board.Name = createBoardDTO.Name;
                board.Status = createBoardDTO.Status;
                board.ProjectId = createBoardDTO.ProjectId;
                board.BoardId = 0;
                board.CreatedDatetime = DateTime.Now;
                await _unitOfWork.BoardRepo.AddAsync(board);
                response.Data = board.BoardId;
                response.Success = true;
                response.Message = "Board created successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to create board: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<PaginationModel<BoardDTO>>> GetPaginatedBoardsByProjectId(int projectId, int page = 1, int pageSize = 20)
        {
            var response = new ServiceResponse<PaginationModel<BoardDTO>>();

            try
            {
                var boards = await _unitOfWork.BoardRepo.GetBoardsByProjectId(projectId);
                var boardDTOs = _mapper.Map<List<BoardDTO>>(boards);
                response.Data = await Pagination.GetPagination(boardDTOs, page, pageSize);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to get boards: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<List<BoardDTO>>> GetBoardsByProjectId(int projectId)
        {
            var response = new ServiceResponse<List<BoardDTO>>();

            try
            {
                var boards = await _unitOfWork.BoardRepo.GetBoardsByProjectId(projectId);
                var boardDTOs = _mapper.Map<List<BoardDTO>>(boards);
                response.Data = boardDTOs;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to get boards: {ex.Message}";
            }
            return response;
        }


        public async Task<ServiceResponse<string>> UpdateBoard(int boardId, CreateBoardDTO createBoardDTO)
        {
            var response = new ServiceResponse<string>();

            try
            {
                var validationContext = new ValidationContext(createBoardDTO);
                var validationResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(createBoardDTO, validationContext, validationResults, true))
                {
                    var errorMessages = validationResults.Select(r => r.ErrorMessage);
                    response.Success = false;
                    response.Message = string.Join("; ", errorMessages);
                    return response;
                }

                var existingBoard = await _unitOfWork.BoardRepo.GetByIdAsync(boardId);
                if (existingBoard == null)
                {
                    response.Success = false;
                    response.Message = "Board not found";
                    return response;
                }
                var existingProject = await _unitOfWork.ProjectRepo.GetByIdAsync(createBoardDTO.ProjectId);
                if (existingProject == null)
                {
                    await _unitOfWork.BoardRepo.Remove(existingBoard);
                    response.Success = false;
                    response.Message = "Project not found";
                    return response;
                }

                existingBoard.Label = createBoardDTO.Label;
                existingBoard.Name = createBoardDTO.Name;
                existingBoard.Status = createBoardDTO.Status;
                existingBoard.ProjectId = createBoardDTO.ProjectId;

                await _unitOfWork.BoardRepo.Update(existingBoard);
                response.Data = "Board updated successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to update board: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<string>> RemoveBoard(int boardId)
        {
            var response = new ServiceResponse<string>();

            try
            {
                var existingBoard = await _unitOfWork.BoardRepo.GetByIdAsync(boardId);
                if (existingBoard == null)
                {
                    response.Success = false;
                    response.Message = "Board not found";
                    return response;
                }
                var existingCards = await _unitOfWork.CardRepo.GetCardsByBoardId(boardId);
                if (!existingCards.IsNullOrEmpty())
                {
                    foreach (Card card in existingCards)
                    {
                        await _unitOfWork.CardRepo.Remove(card);
                    }
                }
                await _unitOfWork.BoardRepo.Remove(existingBoard);
                response.Data = "Board removed successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to remove board: {ex.Message}";
            }

            return response;
        }

    }
}
