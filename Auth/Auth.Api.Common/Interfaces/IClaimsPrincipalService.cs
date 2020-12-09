using System.Collections.Generic;
using System.Security.Claims;

namespace Elaia.Auth.Api.Common.Interfaces
{
    public interface IClaimsPrincipalService
    {
        IEnumerable<Claim> Claims { get; }
        int UserId { get; }
        bool IsAuthenticated { get; }
        bool IsInRole(string role);
    }
}
    