﻿using Application;
using Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiContext _apiContext;
        private readonly IUserRepo _userRepository;
        private readonly ITeamRepo _teamRepo;
        private readonly ITokenRepo _tokenRepo;
        private readonly ITeamMemberRepo _teamMemberRepo;

        public UnitOfWork(ApiContext apiContext, IUserRepo userRepository, ITokenRepo tokenRepo, ITeamRepo team, ITeamMemberRepo teamMemberRepo)
        {
            _apiContext = apiContext;
            _tokenRepo = tokenRepo;
            _userRepository = userRepository;
            _teamRepo = team;
            _teamMemberRepo = teamMemberRepo;
        }

        public IUserRepo UserRepository => _userRepository;

        public ITokenRepo TokenRepo => _tokenRepo;
        public ITeamRepo TeamRepository => _teamRepo;
        public ITeamMemberRepo TeamMemberRepo => _teamMemberRepo;
        public async Task<int> SaveChangeAsync()
        {
            try
            {
                return await _apiContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log exception details here
                throw new ApplicationException("An error occurred while saving changes.", ex);
            }
        }
    }
}
