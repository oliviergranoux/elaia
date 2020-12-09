using Microsoft.Extensions.Logging;
using System;
using Auth.Business.Common.Models;
using Auth.Business.Interfaces;
// using Auth.Api.Common.Interfaces;

namespace Auth.Business.Managers
{
    public class UserManager : IUserManager
    {
        // private readonly Data.repositories.IUserRepository _userRepository;
        private readonly ILogger<UserManager> _logger;
        // private readonly IClaimsPrincipalService _claimsPrincipalService;

        public UserManager(/*Data.repositories.IUserRepository userRopository, IClaimsPrincipalService claimsPrincipalService, */ILogger<UserManager> logger)
        {
            // _userRepository = userRopository;

            // _claimsPrincipalService = claimsPrincipalService;

            _logger = logger;
        }

        public User GetUser(Auth.Api.Common.Models.Login login)
        {
            if (login == null)
                throw new ArgumentNullException(nameof(login)); 

            // _logger.LogTrace("logging trace ...");
            // _logger.LogDebug( "logging debug ...");
            // _logger.LogInformation("logging Info ...");
            // _logger.LogWarning("logging warning ...");
            // _logger.LogError("logging error ...");
            // _logger.LogCritical("logging critical ...");

            // _userRepository.GetUser

            if (login.Username == "test" && login.Password == "pwd")
            {
               return new User
               {
                   Id = 123,
                   FullName = "Mistinguette..."
               };
            } 

            return null;
        }
    }
}