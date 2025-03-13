﻿using Application.ViewModels.CategoryDTO;
using Application.ViewModels.GoalDTO;
using Application.ViewModels.ProjectDTO;
using Application.ViewModels.RewardDTO;
﻿using Application.ViewModels.CommentDTO;
using Application.ViewModels.PostDTO;
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
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UpdateUserDTO>().ReverseMap();
            CreateMap<Project, CreateProjectDto>().ReverseMap();
            CreateMap<Project, UpdateProjectDto>().ReverseMap();
            CreateMap<Category, AddCategory>().ReverseMap();
            CreateMap<Reward, AddReward>().ReverseMap();
            CreateMap<Goal, CreateGoal>().ReverseMap();
            CreateMap<User, PostUserDTO>()
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.Fullname))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar ?? string.Empty))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
            CreateMap<Post, CreatePostDTO>().ReverseMap();
            CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

        }
    }
}
