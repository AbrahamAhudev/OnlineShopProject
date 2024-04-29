using OnlineShopProject.Server.Data;
using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;

namespace OnlineShopProject.Server.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly DataContext _Context;


        public UserRepository(DataContext context)
        {
            _Context = context;
        }

        public bool CreateUser(User user)
        {
           _Context.Add(user);

            return Save();
        }

        public bool DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _Context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool UserExists(int id)
        {
            return _Context.Users.Any(u => u.Id == id);
        }
    }
}
