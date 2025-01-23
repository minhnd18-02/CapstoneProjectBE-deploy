using Application.Commons;
using Application.IService;
using Application.ServiceResponse;
using Application.Utils;
using Application.ViewModels.UserDTO;
using AutoMapper;
using Domain.Entities;
using Newtonsoft.Json.Linq;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
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
                var existEmail = await _unitOfWork.UserRepository.CheckEmailAddressExisted(userObject.Email);
                if (existEmail)
                {
                    response.Success = false;
                    response.Message = "Email is already existed";
                    return response;
                }

                var userAccountRegister = _mapper.Map<User>(userObject);
                userAccountRegister.Password = HashPassWithSHA256.HashWithSHA256(userObject.Password);

                //userAccountRegister.Role = "Customer";
                await _unitOfWork.UserRepository.AddAsync(userAccountRegister);
                //Create Token
                var confirmationToken = new Token
                {
                    TokenValue = Guid.NewGuid().ToString(),
                    Type = "confirmation",
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                    UserId = userAccountRegister.UserId
                };
                await _unitOfWork.TokenRepo.AddAsync(confirmationToken);
                //var confirmationLink =
                //    $"https://koifarmmanagement-axevbhdzh9edauf8.eastus-01.azurewebsites.net/confirm?token={confirmationToken.TokenValue}";

                //SendMail
                //var emailSend = await SendMail.SendConfirmationEmail(userObject.Email, confirmationLink);
                //if (!emailSend)
                //{
                //    response.Success = false;
                //    response.Message = "Error when send mail";
                //    return response;
                //}

                var accountRegistedDTO = _mapper.Map<RegisterDTO>(userAccountRegister);
                response.Success = true;
                response.Data = accountRegistedDTO;
                response.Message = "Register successfully.";
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
                var userLogin = await _unitOfWork.UserRepository.GetUserByEmailAddressAndPasswordHash(userObject.Username, passHash);
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
                var userId = userLogin.UserId;
                var tokenJWT = userLogin.GenerateJsonWebToken(_config, _config.JWTSection.SecretKey, DateTime.Now);
                response.Success = true;
                response.Message = "Login successfully";
                response.DataToken = tokenJWT;
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
    }
}
