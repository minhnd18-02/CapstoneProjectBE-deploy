﻿using Application.ServiceResponse;
using Application.ViewModels.CommentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface ICommentService
    {
        public Task<ServiceResponse<int>> CreatePostComment(CreatePostCommentDTO createCardDTO);
        public Task<ServiceResponse<int>> CreateProjectComment(CreateProjectCommentDTO createProjectCommentDTO);
        public Task<ServiceResponse<PaginationModel<CommentDTO>>> GetPaginatedCommentsByProjectId(int projectId, int page = 1, int pageSize = 20);
        public  Task<ServiceResponse<List<CommentDTO>>> GetCommentsByProjectId(int projectId);
        public Task<ServiceResponse<PaginationModel<CommentDTO>>> GetPaginatedCommentsByPostId(int postId, int page = 1, int pageSize = 20);
        public Task<ServiceResponse<List<CommentDTO>>> GetCommentsByPostId(int postId);
        public Task<ServiceResponse<PaginationModel<CommentDTO>>> GetPaginatedCommentsByUserId(int userId, int page = 1, int pageSize = 20);
        public Task<ServiceResponse<List<CommentDTO>>> GetCommentsByUserId(int userId);
        public Task<ServiceResponse<string>> UpdateComment(int commentId, UpdateCommentDTO updateCommentDTO);
        public Task<ServiceResponse<string>> RemoveComment(int commentId);
        public Task<ServiceResponse<string>> SoftRemoveComment(int commentId);
    }
}