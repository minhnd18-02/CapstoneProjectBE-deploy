﻿using CloudinaryDotNet.Actions;
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
        public string PaymentId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public virtual Pledge Pledge { get; set; } = null!;
    }
}
