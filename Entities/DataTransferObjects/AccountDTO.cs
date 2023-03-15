﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class AccountDTO
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string? AccountType { get; set; }
    }
}
