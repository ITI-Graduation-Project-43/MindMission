﻿using MindMission.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.DTOs
{
    public class UserAccountDto
    {
        public string UserId { get; set; } = string.Empty;
        public int accountId { get; set; }
        public string accountLink { get; set; } = string.Empty;
         

    }
}
