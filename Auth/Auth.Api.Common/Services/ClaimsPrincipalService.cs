using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Auth.Api.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Auth.Api.Common.Services
{

    public class ClaimsPrincipalService : IClaimsPrincipalService
    {
        private readonly ClaimsPrincipal _user;

        public IEnumerable<Claim> Claims {
            get { return _user.Claims; }
        }

        public int UserId
        {
            get
            {
                var userId = default(int);

                if (_user.HasClaim(x => x.Type == "UserId"))
                    Int32.TryParse(_user.Claims.First(x => x.Type == "UserId").Value, out userId);

                return userId;
            }
        }

        public bool IsAuthenticated
        {
            get { return _user.Identity.IsAuthenticated; }
        }

        public ClaimsPrincipalService(IHttpContextAccessor httpContextAccessor)
        {
            _user = httpContextAccessor.HttpContext.User;
        }
        
        public bool IsInRole(string role)
        {
            if (string.IsNullOrEmpty(role))
                return false;
            
            return _user.IsInRole(role);
        }
    }
}