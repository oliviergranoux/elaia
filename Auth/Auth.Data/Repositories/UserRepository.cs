

using System.Linq;
using Auth.Data.Common.Models;

namespace Auth.Data.repositories
{
    public interface IUserRepository
    {
        User GetUser(int userId);
    }

    public class UserRepository
    {
        public UserRepository()
        {

        }

        public User GetUser(string username, string password)
        {
            using (var context = new AuthContext())
            {
                return context.Users.Where(m => m.FullName == username).FirstOrDefault();
                // return context.Users.ToList().Where(m => m.Id == 123).FirstOrDefault();
            }
        }
    }
}