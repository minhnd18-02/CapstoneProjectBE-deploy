using Application.ServiceResponse;
using Application.ViewModels.VnpayDTO;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentUrl(double money, string description, HttpContext httpContext, int projectId, int userId);
        PaymentResult ProcessPaymentResult(IQueryCollection query);
    }
}
