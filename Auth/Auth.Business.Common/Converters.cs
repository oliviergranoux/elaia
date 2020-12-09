using Elaia.Auth.Business.Common.Models;

namespace Elaia.Auth.Business.Common.Converters
{
    public class Converter
    {
        public User ConvertTo(Elaia.Auth.Data.Common.Models.User user)
        {
            if (user == null)
                return null;

            return new User
            {
                Id = user.Id,
                FullName = user.FullName
            };
        }

        public Elaia.Auth.Data.Common.Models.User ConvertTo(User user)
        {
            if (user == null)
                return null;

            return new Elaia.Auth.Data.Common.Models.User
            {
                Id = user.Id,
                FullName = user.FullName
            };
        }
    }
}