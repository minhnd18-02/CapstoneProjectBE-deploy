using Application.IService;
using Application.ServiceResponse;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using PayPal.Api;
using PayPal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PaypalPaymentService : IPaypalPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PaypalPaymentService(IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceResponse<string>> CreatePaymentAsync(int projectId, decimal amount, string returnUrl, string cancelUrl)
        {
            var response = new ServiceResponse<string>();

            try
            {
                Domain.Entities.Pledge pledge = new Domain.Entities.Pledge 
                {
                    PledgeId = 0,
                    UserId = 26,
                    Amount = amount,
                    ProjectId = projectId
                };

                decimal conversionRate = 23000m;
                decimal totalAmountVND = amount;
                decimal totalAmountUSD = totalAmountVND / conversionRate;
                string totalAmountInUSD = totalAmountUSD.ToString("F2");

                var apiContext = new APIContext(new OAuthTokenCredential(
                    _configuration["PayPal:ClientId"],
                    _configuration["PayPal:ClientSecret"]
                ).GetAccessToken());

                var payment = new Payment
                {
                    intent = "sale",
                    payer = new Payer { payment_method = "paypal" },
                    transactions = new List<Transaction>
            {
                new Transaction
                {
                    description = $"Pledge {pledge.PledgeId} - Payment",
                    invoice_number = pledge.PledgeId.ToString(),
                    amount = new Amount
                    {
                        currency = "USD",
                        total = totalAmountInUSD
                    },
                    custom = pledge.UserId.ToString()
                }
            },
                    redirect_urls = new RedirectUrls
                    {
                        cancel_url = cancelUrl,
                        return_url = returnUrl
                    }
                };

                var createdPayment = payment.Create(apiContext);

                if (createdPayment != null && createdPayment.links != null)
                {
                    var approvalLink = createdPayment.links.FirstOrDefault(link => link.rel == "approval_url")?.href;

                    if (approvalLink != null)
                    {

                        response.Success = true;
                        response.Message = "Payment created successfully.";
                        response.Data = approvalLink;
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Approval URL not found.";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Failed to create payment.";
                }

            }
            catch (PayPalException payPalEx)
            {
                response.Success = false;
                response.Error = $"PayPal error: {payPalEx.Message}";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = $"Failed to create payment: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<Payment>> ExecutePaymentAsync(string paymentId, string payerId)
        {
            var response = new ServiceResponse<Payment>();
            try
            {
                var apiContext = new APIContext(new OAuthTokenCredential(
                    _configuration["PayPal:ClientId"],
                    _configuration["PayPal:ClientSecret"]
                ).GetAccessToken());

                var payment = Payment.Get(apiContext, paymentId);

                if (payment == null || string.IsNullOrEmpty(payment.state) || !(payment.transactions.Count > 0) || !int.TryParse(payment.transactions.First().custom, out int userId) || !(userId > 0) || !int.TryParse(payment.transactions.First().invoice_number, out int orderId))
                {
                    response.Success = false;
                    response.Message = "Payment not found or invalid.";
                    return response;
                }

                if (payment.state != "created")
                {
                    response.Success = false;
                    response.Message = "Payment is not approved yet.";
                    return response;
                }

                var pledgeitem = await ValidatePledge(userId);
                var pledge = pledgeitem.Item1;

                if (!string.IsNullOrEmpty(errorMessage) || pledge == null || pledge.PledgeId != orderId)
                {
                    if (payment.state == "authorized")
                    {
                        var authorization = new Authorization() { id = payment.id };
                        var voidResponse = authorization.Void(apiContext);
                    }
                    response.Success = false;
                    response.Message = string.IsNullOrEmpty(pledgeitem.Item2) ? "The order is invalid. Please try again." : pledgeitem.Item2;
                    return response;

                }

                //order.OrderStatus = true;
                //order.OrderDate = DateTime.Now;
                await _unitOfWork.PledgeRepo.UpdateAsync(pledge);

                // Prepare to execute the payment
                var paymentExecution = new PaymentExecution() { payer_id = payerId };
                var executedPayment = payment.Execute(apiContext, paymentExecution);

                if (executedPayment == null || string.IsNullOrEmpty(executedPayment.state) || executedPayment.state == "failed")
                {
                    response.Success = false;
                    response.Message = "Payment executed unsuccessfully.";
                    return response;
                }

                if (executedPayment == null || string.IsNullOrEmpty(executedPayment.state) || executedPayment.state == "failed")
                {
                    response.Success = false;
                    response.Message = "Payment executed unsuccessfully.";
                    return response;
                }

                response.Success = true;
                response.Message = "Payment successful.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to execute payment: {ex.Message}";
            }

            return response;
        }

        private string errorMessage = string.Empty;

        public async Task<(Domain.Entities.Pledge?, string)> ValidatePledge(int pledgeId)
        {
            try
            {
                var pledge = await _unitOfWork.PledgeRepo.GetPledgeByIdAsync(pledgeId);

                if (pledge == null)
                {
                    errorMessage = "The details of the pledge have been changed and the payment cannot be proceeded with.";
                    return (null, errorMessage);
                }

                int i = 0;

                if (pledge.PledgeId == null)
                {
                    errorMessage = "The cart is invalid.";
                    return (null, errorMessage);
                }

                return (pledge, string.Empty);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return (null, errorMessage);
            }
        }
    }
}
