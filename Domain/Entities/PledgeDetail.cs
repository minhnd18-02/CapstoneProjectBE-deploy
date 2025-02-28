using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PledgeDetail
    {
        public int PledgeId { get; set; }
        public int PaymentLinkInformationId { get; set; }

        public virtual Pledge Pledge { get; set; } = null!;
        public virtual PaymentLinkInformation PaymentLinkInformation { get; set; } = null!;
    }
}
