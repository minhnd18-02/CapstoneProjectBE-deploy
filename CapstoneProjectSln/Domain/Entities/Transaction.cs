using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int PaymentLinkInformationId { get; set; }
        public string Reference { get; set; } = string.Empty;
        public int Amount { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TransactionDateTime { get; set; } = string.Empty;
        public string? VirtualAccountName { get; set; }
        public string? VirtualAccountNumber { get; set; }
        public string? CounterAccountBankId { get; set; }
        public string? CounterAccountBankName { get; set; }
        public string? CounterAccountName { get; set; }
        public string? CounterAccountNumber { get; set; }

        public virtual PaymentLinkInformation PaymentLinkInformation { get; set; } = null!;
    }
}
