﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IUserService
    {
        
        public Guid Id { get; }
        public string UserName { get; }
        public string Email { get; }
    }
}
