﻿using Bobkov.BL.DTO;
using Bobkov.BL.Infrastructure;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bobkov.BL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
    }
}
