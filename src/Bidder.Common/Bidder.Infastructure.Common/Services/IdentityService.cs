﻿using Bidder.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Bidder.Infastructure.Common.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public IdentityService(IHttpContextAccessor _httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;
        }
        public Guid GetUserId()
        {
            var data = httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.UserData)?.Value;
            return data == null ? Guid.Empty : Guid.Parse(data);
        }

        public string GetUserName()
        {
            var data = httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return data;
        }

        public string GetUserEmail()
        {
            var data = httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Email)?.Value;
            return data;
        }
    }
}
