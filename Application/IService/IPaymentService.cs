using Application.ServiceResponse;
using Application.ViewModels.VnpayDTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IPaymentService
    {
        string CreatePaymentUrl(double money, string description, HttpContext httpContext, int projectId);
        PaymentResult ProcessPaymentResult(IQueryCollection query);
    }
}
