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
            _Context.Remove(user);

            return Save();
        }

        public User GetUserById(int id)
        {
            return _Context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _Context.Users.ToList();
        }

        public bool Save()
        {
            var saved = _Context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateUser(User user)
        {
            _Context.Update(user);

            return Save();
        }

        public bool UserExists(int id)
        {
            return _Context.Users.Any(u => u.Id == id);
        }

    }
}
