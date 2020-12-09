using Elaia.Auth.Business.Common.Models;

namespace Elaia.Auth.Business.Interfaces
{
    public interface IUserManager
    {
        User GetUser(Elaia.Auth.Api.Common.Models.Login login);
    }
}