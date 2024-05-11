using OnlineShopProject.Server.Models;

namespace OnlineShopProject.Server.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();

        User GetUserById(int id);

        bool UserExists(int id);

        bool CreateUser(User user);

        bool UpdateUser(User user);

        bool DeleteUser(User user);

        bool Save();

        string EncryptPassword(string password);
    }
}
