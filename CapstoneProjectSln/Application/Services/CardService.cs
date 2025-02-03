using Application.Commons;
using Application.IService;
using Application.ServiceResponse;
using Application.Utils;
using Application.ViewModels.CardDTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Application.Services
{
    public class CardService : ICardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CardService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<int>> CreateCard(CreateCardDTO createCardDTO)
        {
            var response = new ServiceResponse<int>();

            try
            {
                var validationContext = new ValidationContext(createCardDTO);
                var validationResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(createCardDTO, validationContext, validationResults, true))
                {
                    var errorMessages = validationResults.Select(r => r.ErrorMessage);
                    response.Success = false;
                    response.Message = string.Join("; ", errorMessages);
                    return response;
                }

                var existingBoard = await _unitOfWork.BoardRepo.GetByIdAsync(createCardDTO.BoardId);
                if (existingBoard == null)
                {
                    response.Success = false;
                    response.Message = "Board not found";
                    return response;
                }

                Card card = new Card();
                card.BoardId = createCardDTO.BoardId;
                card.Deadline = createCardDTO.Deadline;
                card.Name = createCardDTO.Name;
                card.Status = createCardDTO.Status;
                card.Description = createCardDTO.Description;
                card.CardId = 0;
                card.CreatedDatetime = DateTime.Now;
                await _unitOfWork.CardRepo.AddAsync(card);
                response.Data = card.CardId;
                response.Success = true;
                response.Message = "Card created successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to create card: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<PaginationModel<CardDTO>>> GetPaginatedCardsByBoardId(int boardId, int page = 1, int pageSize = 20)
        {
            var response = new ServiceResponse<PaginationModel<CardDTO>>();

            try
            {
                var cards = await _unitOfWork.CardRepo.GetCardsByBoardId(boardId);
                var cardDTOs = _mapper.Map<List<CardDTO>>(cards);
                response.Data = await Pagination.GetPagination(cardDTOs, page, pageSize);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to get cards: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<List<CardDTO>>> GetCardsByBoardId(int boardId)
        {
            var response = new ServiceResponse<List<CardDTO>>();

            try
            {
                var cards = await _unitOfWork.CardRepo.GetCardsByBoardId(boardId);
                var cardDTOs = _mapper.Map<List<CardDTO>>(cards);
                response.Data = cardDTOs;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to get cards: {ex.Message}";
            }
            return response;
        }


        public async Task<ServiceResponse<string>> UpdateCard(int cardId, CreateCardDTO createCardDTO)
        {
            var response = new ServiceResponse<string>();

            try
            {
                var validationContext = new ValidationContext(createCardDTO);
                var validationResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(createCardDTO, validationContext, validationResults, true))
                {
                    var errorMessages = validationResults.Select(r => r.ErrorMessage);
                    response.Success = false;
                    response.Message = string.Join("; ", errorMessages);
                    return response;
                }

                var existingCard = await _unitOfWork.CardRepo.GetByIdAsync(cardId);
                if (existingCard == null)
                {
                    response.Success = false;
                    response.Message = "Card not found";
                    return response;
                }
                var existingBoard = await _unitOfWork.BoardRepo.GetByIdAsync(createCardDTO.BoardId);
                if (existingBoard == null)
                {
                    response.Success = false;
                    response.Message = "Board not found";
                    return response;
                }

                existingCard.BoardId = createCardDTO.BoardId;
                existingCard.Deadline = createCardDTO.Deadline;
                existingCard.Name = createCardDTO.Name;
                existingCard.Status = createCardDTO.Status;
                existingCard.Description = createCardDTO.Description;

                await _unitOfWork.CardRepo.Update(existingCard);
                response.Data = "Card updated successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to update card: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<string>> RemoveCard(int cardId)
        {
            var response = new ServiceResponse<string>();

            try
            {
                var existingCard = await _unitOfWork.CardRepo.GetByIdAsync(cardId);
                if (existingCard == null)
                {
                    response.Success = false;
                    response.Message = "Card not found";
                    return response;
                }
                await _unitOfWork.CardRepo.Remove(existingCard);
                response.Data = "Card removed successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to remove card: {ex.Message}";
            }

            return response;
        }

    }
}
