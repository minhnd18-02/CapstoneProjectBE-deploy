using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Report
    {
        public int ReportId { get; set; }
        public int UserId { get; set; }
        public string Detail { get; set; } = string.Empty;
        public DateTime CreateDatetime { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
