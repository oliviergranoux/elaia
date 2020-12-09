using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Api.Services
{

    public interface IAuthenticationService
    {
       string Authenticate(Business.Common.Models.User user);
    }
    
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IOptions<AppSettings> _settings;

        public AuthenticationService(IOptions<AppSettings> settings)
        {
            _settings = settings;
        }

        public string Authenticate(Business.Common.Models.User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = Authenticate(user, tokenHandler);
            return tokenHandler.WriteToken(token);
        }
        
        private SecurityToken Authenticate(Business.Common.Models.User user, JwtSecurityTokenHandler tokenHandler)
        {
            var key = Encoding.ASCII.GetBytes(_settings.Value.JwtSecret);

            // var key = new RsaSecurityKey(RSA.Create(2048));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Role, "Administrator"),
                    // new Claim(ClaimTypes.Role, "Accounter"),
                    new Claim(ClaimTypes.Sid, "Session ID"), /* eecb9bf34bbb4c8eb87dbba3aa1523c6 */
                    new Claim("UserId", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                // SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSsaPssSha256Signature)
            };

            return tokenHandler.CreateToken(tokenDescriptor);
        }
    }
}