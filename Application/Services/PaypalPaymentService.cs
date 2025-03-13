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
using Domain.Entities;

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
        public async Task<ServiceResponse<string>> CreateRefundAsync(int userId, int pledgeId)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var pledge = await _unitOfWork.PledgeRepo.GetPledgeByIdAsync(pledgeId);
                if (pledge == null)
                {
                    response.Success = false;
                    response.Message = "Pledge not found.";
                    return response;
                }

                var user = await _unitOfWork.UserRepo.GetByIdAsync(userId);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                    return response;
                }

                var apiContext = new APIContext(new OAuthTokenCredential(
                    _configuration["PayPal:ClientId"],
                    _configuration["PayPal:ClientSecret"]
                ).GetAccessToken());

                var payout = new Payout
                {
                    sender_batch_header = new PayoutSenderBatchHeader
                    {
                        sender_batch_id = Guid.NewGuid().ToString(),
                        email_subject = "You have a refund from your pledge"
                    },
                    items = new List<PayoutItem>
            {
                new PayoutItem
                {
                    recipient_type = PayoutRecipientType.EMAIL,
                    amount = new Currency
                    {
                        value = pledge.Amount.ToString("F2"),
                        currency = "USD"
                    },
                    receiver = user.Email,
                    note = "Refund for your pledge",
                    sender_item_id = pledge.PledgeId.ToString()
                }
            }
                };

                var createdPayout = payout.Create(apiContext, true);

                if (createdPayout.batch_header.batch_status != "SUCCESS")
                {
                    response.Success = false;
                    response.Message = "Failed to create payout.";
                    return response;
                }

                pledge.Project.TotalAmount -= pledge.Amount;
                pledge.Amount = 0;
                await _unitOfWork.PledgeRepo.UpdateAsync(pledge);

                response.Success = true;
                response.Message = "Payout created successfully.";
            }
            catch (PayPalException payPalEx)
            {
                response.Success = false;
                response.Message = $"PayPal error: {payPalEx.Message}";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to create payout: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<string>> CreatePaymentAsync(int userId, int projectId, decimal amount, string returnUrl, string cancelUrl)
        {
            var response = new ServiceResponse<string>();

            try
            {

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
                    description = $"Pledge to project {projectId} by user {userId} - Payment",
                    invoice_number = $"P{projectId}-U{userId}",
                    amount = new Amount
                    {
                        currency = "USD",
                        total = totalAmountInUSD
                    },
                    note_to_payee = projectId.ToString(),
                    custom = userId.ToString()
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

                if (payment == null || string.IsNullOrEmpty(payment.state) || !(payment.transactions.Count > 0) || !int.TryParse(payment.transactions.First().custom, out int userId) || !(userId > 0) || !int.TryParse(payment.transactions.First().note_to_payee, out int projectId))
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

                var pledgeitem = await ValidatePledge(1);
                var pledge = pledgeitem.Item1;

                if (!string.IsNullOrEmpty(errorMessage) || pledge == null)
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

                var existingPledge = await _unitOfWork.PledgeRepo.GetPledgeByUserIdAndProjectIdAsync(userId, projectId);

                if (existingPledge == null)
                {
                    Domain.Entities.Pledge newPledge = new Domain.Entities.Pledge
                    {
                        UserId = userId,
                        Amount = 0,
                        ProjectId = projectId
                    };

                    await _unitOfWork.PledgeRepo.AddAsync(pledge);

                    Domain.Entities.PledgeDetail pledgeDetail = new Domain.Entities.PledgeDetail
                    {
                        PledgeId = pledge.PledgeId,
                        PaymentId = paymentId,
                        Status = "pledged"
                    };

                    await _unitOfWork.PledgeDetailRepo.AddAsync(pledgeDetail);
                }
                else
                {
                    existingPledge.Amount += decimal.Parse(payment.transactions.First().amount.total);
                    await _unitOfWork.PledgeRepo.UpdateAsync(existingPledge);

                    Domain.Entities.PledgeDetail pledgeDetail = new Domain.Entities.PledgeDetail
                    {
                        PledgeId = pledge.PledgeId,
                        PaymentId = paymentId,
                        Status = "pledged"
                    };

                    await _unitOfWork.PledgeDetailRepo.AddAsync(pledgeDetail);

                }

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
