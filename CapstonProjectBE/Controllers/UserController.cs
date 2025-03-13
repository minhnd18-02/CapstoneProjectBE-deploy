﻿using Application.IService;
using Application.ViewModels.UserDTO;
using AutoMapper;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Infrastructure;
using Microsoft.Extensions.Options;
using Application.ViewModels;
using Application.Services;

namespace CapstonProjectBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Cloudinary _cloudinary;
        private readonly IUserService _userService;
        private readonly IAuthenService _authenService;
        private readonly IMapper _mapper;
        private readonly ApiContext _context;

        public UserController(IOptions<Cloud> config, IUserService userService, IMapper mapper, ApiContext context, IAuthenService authenService)
        {
            var cloudinaryAccount = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(cloudinaryAccount);
            _userService = userService;
            _mapper = mapper;
            _context = context;
            _authenService = authenService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var response = await _userService.GetAllUserAsync();
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Customer, Admin")]
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById()
        {
            var user = await _authenService.GetUserByTokenAsync(HttpContext.User);
            if (user == null)
            {
                return Unauthorized();
            }
            var response = await _userService.GetUserByIdAsync(user.UserId);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO UpdateUser)
        {
            var user = await _authenService.GetUserByTokenAsync(HttpContext.User);
            if (user == null)
            {
                return Unauthorized();
            }
            var response = await _userService.UpdateUserAsync(UpdateUser, user.UserId);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("avatar")]
        public async Task<IActionResult> UpdateUserAvatar(IFormFile file)
        {
            var AuthorizeUser = await _authenService.GetUserByTokenAsync(HttpContext.User);
            if (AuthorizeUser == null)
            {
                return Unauthorized();
            }
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Crop("fill").Gravity("face")
                    };
                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }
            }

            if (uploadResult.Url == null)
                return BadRequest("Could not upload image");

            // Update the image URL in the database
            AuthorizeUser.Avatar = uploadResult.Url.ToString();
            await _context.SaveChangesAsync();

            return Ok(new { imageUrl = AuthorizeUser.Avatar });
        }
    }
}
