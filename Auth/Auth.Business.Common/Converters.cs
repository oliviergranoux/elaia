using Auth.Business.Common.Models;

namespace Auth.Business.Common.Converters
{
    public class Converter
    {
        public User ConvertTo(Auth.Data.Common.Models.User user)
        {
            if (user == null)
                return null;

            return new User
            {
                Id = user.Id,
                FullName = user.FullName
            };
        }

        public Auth.Data.Common.Models.User ConvertTo(User user)
        {
            if (user == null)
                return null;

            return new Data.Common.Models.User
            {
                Id = user.Id,
                FullName = user.FullName
            };
        }
    }
}