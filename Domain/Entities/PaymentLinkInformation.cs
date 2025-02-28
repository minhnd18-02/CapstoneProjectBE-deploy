using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PaymentLinkInformation
    {
        public int PaymentLinkInformationId { get; set; }
        public int UserId { get; set; }
        public string Id { get; set; } = string.Empty;
        public long OrderCode { get; set; }
        public int Amount { get; set; }
        public int AmountPaid { get; set; }
        public int AmountRemaining { get; set; }
        public string Status { get; set; } = string.Empty;
        public string CreateAt { get; set; } = string.Empty;
        public string? CancelAt { get; set; }
        public string? CancellationReason { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public virtual PledgeDetail PledgeDetail { get; set; } = null!;

    }
}
