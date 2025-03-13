﻿using Application.Commons;
using Application.IService;
using Application.ServiceResponse;
using Application.Utils;
using Application.ViewModels.UserDTO;
using AutoMapper;
using Domain.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthenService : IAuthenService
    {
        private readonly AppConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthenService(AppConfiguration config, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _config = config;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<RegisterDTO>> RegisterAsync(RegisterDTO userObject)
        {
            var response = new ServiceResponse<RegisterDTO>();
            try
            {
                var existEmail = await _unitOfWork.UserRepo.CheckEmailAddressExisted(userObject.Email);
                if (existEmail)
                {
                    response.Success = false;
                    response.Message = "Email is already existed";
                    return response;
                }

                var userAccountRegister = _mapper.Map<User>(userObject);
                userAccountRegister.Password = HashPassWithSHA256.HashWithSHA256(userObject.Password);
                userAccountRegister.Role = "Customer";
                userAccountRegister.CreatedDatetime = DateTime.UtcNow;
                await _unitOfWork.UserRepo.AddAsync(userAccountRegister);

                // Create Token
                var confirmationToken = new Token
                {
                    TokenValue = Guid.NewGuid().ToString(),
                    Type = "confirmation",
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                    UserId = userAccountRegister.UserId
                };
                await _unitOfWork.TokenRepo.AddAsync(confirmationToken);

                // Construct Confirmation Link
                var confirmationLink = $"https://localhost:50875/swagger/confirm?token={confirmationToken.TokenValue}";

                // Send Mail
                var emailSend = await EmailSender.SendConfirmationEmail(userObject.Email, confirmationLink);
                if (!emailSend)
                {
                    response.Success = false;
                    response.Message = "Error when sending mail";
                    return response;
                }

                var accountRegistedDTO = _mapper.Map<RegisterDTO>(userAccountRegister);
                response.Success = true;
                response.Data = accountRegistedDTO;
                response.Message = "Register successfully. Please check your email to confirm your account.";
            }
            catch (DbException e)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { e.Message };
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { e.Message };
            }

            return response;
        }

        public async Task<TokenResponse<string>> LoginAsync(LoginUserDTO userObject)
        {
            var response = new TokenResponse<string>();
            try
            {
                var passHash = HashPassWithSHA256.HashWithSHA256(userObject.Password);
                var userLogin = await _unitOfWork.UserRepo.GetUserByEmailAddressAndPasswordHash(userObject.Username, passHash);
                if (userLogin == null)
                {
                    response.Success = false;
                    response.Message = "Invalid username or password";
                    return response;
                }
                var token = await _unitOfWork.TokenRepo.GetTokenByUserIdAsync(userLogin.UserId);
                if (token != null && token.TokenValue != "success")
                {
                    response.Success = false;
                    response.Message = "Please confirm via link in your mail";
                    return response;
                }
                var auth = userLogin.Role;
                var userId = userLogin.UserId;
                var tokenJWT = userLogin.GenerateJsonWebToken(_config, _config.JWTSection.SecretKey, DateTime.Now);
                response.Success = true;
                response.Message = "Login successfully";
                response.DataToken = tokenJWT;
                response.Role = auth;
                response.HintId = userId;
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<User> GetUserByTokenAsync(ClaimsPrincipal claims)
        {
            if (claims == null)
            {
                throw new ArgumentNullException("Invalid token");
            }
            var userId = claims.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
            {
                throw new ArgumentException("No user can be found");
            }

            var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (user == null)
            {
                throw new NullReferenceException("No user can be found");
            }
            return user;
        }
        public async Task<ServiceResponse<string>> ResendConfirmationTokenAsync(string email)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var user = await _unitOfWork.UserRepo.GetByEmailAsync(email);
                if (user == null)
                {
                    response.Success = false;
                    response.Error = "Không tìm thấy người dùng với email này.";
                    return response;
                }
                var token = await _unitOfWork.TokenRepo.FindByConditionAsync(user.UserId, "confirmation");
                if (token != null && token.TokenValue == "success")
                {
                    response.Success = false;
                    response.Message = "Email của bạn đã được xác nhận.";
                    return response;
                }
                if (DateTime.UtcNow > token.ExpiresAt)
                {
                    await _unitOfWork.TokenRepo.DeleteTokenAsync(token);
                    var newToken = new Token
                    {
                        TokenValue = Guid.NewGuid().ToString(),
                        Type = "confirmation",
                        CreatedAt = DateTime.UtcNow,
                        ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                        UserId = user.UserId
                    };

                    await _unitOfWork.TokenRepo.AddAsync(newToken);

                    var confirmationLink = $"https://koifarmmanagement-axevbhdzh9edauf8.eastus-01.azurewebsites.net/confirm?token={newToken.TokenValue}";
                    var emailSend = await EmailSender.SendConfirmationEmail(user.Email, confirmationLink);

                    if (!emailSend)
                    {
                        response.Success = false;
                        response.Message = "Gửi email thất bại.";
                        return response;
                    }

                    response.Success = true;
                    response.Message = "Email xác nhận mới đã được gửi.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Token xác nhận của bạn vẫn còn hiệu lực. Vui lòng kiểm tra email.";
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Đã xảy ra lỗi.";
                response.ErrorMessages = new List<string> { e.Message };
            }
            return response;
        }
    }
}
