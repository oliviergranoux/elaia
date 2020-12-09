using Auth.Business.Common.Models;

namespace Auth.Business.Interfaces
{
    public interface IUserManager
    {
        User GetUser(Auth.Api.Common.Models.Login login);
    }
}