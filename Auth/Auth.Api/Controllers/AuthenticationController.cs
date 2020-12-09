using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

using Elaia.Auth.Api.Services;
using Elaia.Auth.Api.Common.Models;
using Elaia.Auth.Api.Common.Services;
using Elaia.Auth.Api.Common.Interfaces;
using Elaia.Auth.Business.Interfaces;

namespace Elaia.Auth.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {       
        #region Properties
        private readonly IUserManager _userManager;

        private readonly IAuthenticationService _authService;
        private IClaimsPrincipalService _claimsPrincipalService;

        private readonly IOptions<AppSettings> _settings;
        private readonly ILogger<AuthenticationController> _logger;
        #endregion Properties
        
        [ActivatorUtilitiesConstructor]
        public AuthenticationController(IUserManager userManager, 
            IAuthenticationService authenticationService, 
            IClaimsPrincipalService claimsPrincipalService, 
            IOptions<AppSettings> settings, 
            ILogger<AuthenticationController> logger)
        {
            _userManager = userManager;

            _authService = authenticationService;
            _claimsPrincipalService = claimsPrincipalService;

            _logger = logger;
            _settings = settings;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]Login login)
        {
            if (login == null)
                throw new ArgumentNullException(nameof(login));          

            _logger.LogInformation($"Get login for user '{login.Username}'");

            var user = _userManager.GetUser(login);
            if (user == null)
                return NotFound();
            
            return Ok(_authService.Authenticate(user));
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public IActionResult Test()
        {
            var now = DateTime.UtcNow.ToShortTimeString();

            // _logger.LogInformation($"My UserID: {_claimsPrincipalService.UserId}");

            _logger.LogInformation($"Test {now}: {_settings.Value.Test}");
            return Ok($"{now} {_settings.Value.Test}");
        }
    }
}