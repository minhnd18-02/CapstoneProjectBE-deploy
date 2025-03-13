﻿using Application.ViewModels.CategoryDTO;
using Application.ViewModels.GoalDTO;
using Application.ViewModels.ProjectDTO;
using Application.ViewModels.RewardDTO;
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
            CreateMap<Category, AddCategory>().ReverseMap();
            CreateMap<Reward, AddReward>().ReverseMap();
            CreateMap<Goal, CreateGoal>().ReverseMap();
        }
    }
}
