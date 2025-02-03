using Application.IRepositories;
using Application.IService;
using Application.ServiceResponse;
using Application.ViewModels.TeamDTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TeamService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<TeamDTO>> CreateTeamAsync(TeamDTO teamDTO)
        {
            var response = new ServiceResponse<TeamDTO>();
            try
            {
                var team = _mapper.Map<Team>(teamDTO);
                await _unitOfWork.TeamRepository.AddAsync(team);
                await _unitOfWork.SaveChangeAsync();
                response.Data = _mapper.Map<TeamDTO>(team);
                response.Success = true;
                response.Message = "Team created successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to create team: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<TeamDTO>> GetByIdAsync(int id)
        {
            var response = new ServiceResponse<TeamDTO>();
            try
            {
                var team = await _unitOfWork.TeamRepository.GetByIdIncludingTeamMemberAsync(id);
                if (team == null)
                {
                    response.Success = false;
                    response.Message = "Team not found";
                }
                else
                {
                    response.Data = _mapper.Map<TeamDTO>(team);
                    response.Success = true;
                    response.Message = "Team retrieved successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to get team: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<TeamDTO>>> GetAllAsync()
        {
            var response = new ServiceResponse<IEnumerable<TeamDTO>>();
            try
            {
                var teams = await _unitOfWork.TeamRepository.GetAllIcludingTeamMembersAsync();
                response.Data = _mapper.Map<IEnumerable<TeamDTO>>(teams);
                response.Success = true;
                response.Message = "Teams retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to get teams: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<TeamDTO>> UpdateAsync(int id, TeamDTO teamDTO)
        {
            var response = new ServiceResponse<TeamDTO>();
            try
            {
                var team = await _unitOfWork.TeamRepository.GetByIdAsync(id);
                if (team == null)
                {
                    response.Success = false;
                    response.Message = "Team not found";
                }
                else
                {
                    _mapper.Map(teamDTO, team);
                    await _unitOfWork.TeamRepository.UpdateAsync(team);
                    response.Data = _mapper.Map<TeamDTO>(team);
                    response.Success = true;
                    response.Message = "Team updated successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to update team: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var team = await _unitOfWork.TeamRepository.GetByIdAsync(id);
                if (team == null)
                {
                    response.Success = false;
                    response.Message = "Team not found";
                }
                else
                {
                    await _unitOfWork.TeamRepository.DeleteAsync(id);
                    await _unitOfWork.SaveChangeAsync();
                    response.Data = true;
                    response.Success = true;
                    response.Message = "Team deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to delete team: {ex.Message}";
                response.Data = false;
            }
            return response;
        }
    }
}
