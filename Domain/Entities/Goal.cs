﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Goal
    {
        public int ProjectId { get; set; }
        public decimal Amount { get; set; }
        public virtual Project Project { get; set; } = null!;
    }
}
