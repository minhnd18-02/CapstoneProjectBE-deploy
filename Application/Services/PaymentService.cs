﻿using Application.IService;
using Application.ServiceResponse;
using Application.Utils.Vnpay;
using Application.ViewModels.VnpayDTO;
using AutoMapper;
using CloudinaryDotNet;
using Domain.Entities;
using Domain.Enums.VnpayEnums;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IVnpay _vnpay;
        private readonly IConfiguration _configuration;

        public PaymentService(IVnpay vnPayservice, IConfiguration configuration)
        {
            _vnpay = vnPayservice;
            _configuration = configuration;

            _vnpay.Initialize(_configuration["Vnpay:TmnCode"], _configuration["Vnpay:HashSecret"], _configuration["Vnpay:BaseUrl"], _configuration["Vnpay:CallbackUrl"]);
        }

        public string CreatePaymentUrl(double money, string description, HttpContext httpContext, int projectId)
        {
            var ipAddress = NetworkHelper.GetIpAddress(httpContext); // Lấy địa chỉ IP của thiết bị thực hiện giao dịch

            var request = new PaymentRequest
            {
                PaymentId = DateTime.Now.Ticks,
                Money = money,
                Description = description,
                IpAddress = ipAddress,
                BankCode = BankCode.ANY, // Tùy chọn. Mặc định là tất cả phương thức giao dịch
                CreatedDate = DateTime.Now, // Tùy chọn. Mặc định là thời điểm hiện tại
                Currency = Currency.VND, // Tùy chọn. Mặc định là VND (Việt Nam đồng)
                Language = DisplayLanguage.Vietnamese // Tùy chọn. Mặc định là tiếng Việt
            };
            //Pledge pledge = new Pledge();
            //pledge.Amount = (decimal)money;
            //pledge.ProjectId = projectId;
            //pledge.UserId = userId;
            return _vnpay.GetPaymentUrl(request);
        }

        public PaymentResult ProcessPaymentResult(IQueryCollection query)
        {
            return _vnpay.GetPaymentResult(query);
        }
    }
}
