using Application.ViewModels.ProjectDTO;
﻿using Application.ViewModels.BoardDTO;
using Application.ViewModels.CardDTO;
﻿using Application.ViewModels.TeamDTO;
using Application.ViewModels.TeamMemberDTO;
using Application.ViewModels.UserDTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<User, LoginUserDTO>().ReverseMap();
            CreateMap<Project, CreateProjectDto>().ReverseMap();
            CreateMap<Project, UpdateProjectDto>().ReverseMap();
            CreateMap<Card, CardDTO>().ReverseMap();
            CreateMap<Board, BoardDTO>().ReverseMap();
            CreateMap<Team, TeamDTO>().ReverseMap();
            CreateMap<TeamMember, TeamMemberDTO>().ReverseMap();
        }
    }
}
