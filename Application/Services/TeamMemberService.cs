using Application.IRepositories;
using Application.IService;
using Application.ServiceResponse;
using Application.ViewModels.TeamMemberDTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TeamMemberService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<TeamMemberDTO>> GetByTeamIdAndUserId(int teamId, int userId)
        {
            var response = new ServiceResponse<TeamMemberDTO>();
            try
            {
                var teamMember = await _unitOfWork.TeamMemberRepo.GetByTeamIdAndUserIdIcludingTeamAndUserAsync(teamId, userId);
                if (teamMember == null)
                {
                    response.Success = false;
                    response.Message = "Team member not found";
                }
                else
                {
                    response.Data = _mapper.Map<TeamMemberDTO>(teamMember);
                    response.Success = true;
                    response.Message = "Team member retrieved successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to get team member: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<TeamMemberDTO>>> GetAllByTeamIdAsync(int teamId)
        {
            var response = new ServiceResponse<IEnumerable<TeamMemberDTO>>();
            try
            {
                var teamMembers = await _unitOfWork.TeamMemberRepo.GetAllByTeamIdAsync(teamId);
                response.Data = _mapper.Map<IEnumerable<TeamMemberDTO>>(teamMembers);
                response.Success = true;
                response.Message = "Team members retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to get team members: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<TeamMemberDTO>> AddAsync(TeamMemberDTO teamMemberDTO)
        {
            var response = new ServiceResponse<TeamMemberDTO>();
            try
            {
                var existingTeam = await _unitOfWork.TeamRepository.GetByIdAsync(teamMemberDTO.TeamId);
                if (existingTeam == null)
                {
                    response.Success = false;
                    response.Message = "Team not found";
                    return response;
                }
                var existingUser = await _unitOfWork.UserRepository.GetByIdAsync(teamMemberDTO.UserId);
                if (existingUser == null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                    return response;
                }
                var existingTeamMember = await _unitOfWork.TeamMemberRepo.GetByTeamIdAndUserIdIcludingTeamAndUserAsync(teamMemberDTO.TeamId, teamMemberDTO.UserId);
                if (existingTeamMember == null)
                {
                    response.Success = false;
                    response.Message = "Team member not found";
                    return response;
                }
                var teamMember = _mapper.Map<TeamMember>(teamMemberDTO);
                await _unitOfWork.TeamMemberRepo.AddAsync(teamMember);
                await _unitOfWork.SaveChangeAsync();
                response.Data = _mapper.Map<TeamMemberDTO>(teamMember);
                response.Success = true;
                response.Message = "Team member added successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to add team member: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<TeamMemberDTO>> UpdateAsync(TeamMemberDTO teamMemberDTO)
        {
            var response = new ServiceResponse<TeamMemberDTO>();
            try
            {
                var teamMember = await _unitOfWork.TeamMemberRepo.GetByTeamIdAndUserIdIcludingTeamAndUserAsync(teamMemberDTO.TeamId, teamMemberDTO.UserId);
                if (teamMember == null)
                {
                    response.Success = false;
                    response.Message = "Team member not found";
                    return response;
                }
                var existingTeam = await _unitOfWork.TeamRepository.GetByIdAsync(teamMemberDTO.TeamId);
                if (existingTeam == null)
                {
                    response.Success = false;
                    response.Message = "Team not found";
                    await _unitOfWork.TeamMemberRepo.Remove(teamMember);
                    return response;
                }
                var existingUser = await _unitOfWork.UserRepository.GetByIdAsync(teamMemberDTO.UserId);
                if (existingUser == null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                    await _unitOfWork.TeamMemberRepo.Remove(teamMember);
                    return response;
                }

                _mapper.Map(teamMemberDTO, teamMember);
                await _unitOfWork.TeamMemberRepo.UpdateAsync(teamMember);
                response.Data = _mapper.Map<TeamMemberDTO>(teamMember);
                response.Success = true;
                response.Message = "Team member updated successfully.";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to update team member: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int teamId, int userId)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var teamMember = await _unitOfWork.TeamMemberRepo.GetByTeamIdAndUserIdIcludingTeamAndUserAsync(teamId, userId);
                if (teamMember == null)
                {
                    response.Success = false;
                    response.Message = "Team member not found";
                }
                else
                {
                    await _unitOfWork.TeamMemberRepo.Remove(teamMember);
                    await _unitOfWork.SaveChangeAsync();
                    response.Data = true;
                    response.Success = true;
                    response.Message = "Team member deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to delete team member: {ex.Message}";
                response.Data = false;
            }
            return response;
        }
    }
}
