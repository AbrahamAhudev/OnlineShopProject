using OnlineShopProject.Server.Data;
using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace OnlineShopProject.Server.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly DataContext _Context;


        public UserRepository(DataContext context)
        {
            _Context = context;
        }

        public bool ChangePassword(int userId, string newPassword)
        {
            var user = _Context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            if(newPassword.Length < 6)
            {
                return false;
            }

            user.Password = EncryptPassword(newPassword);

            return UpdateUser(user);    
        }

        public bool CheckPassword(User user, string password)
        {
            if (user.Password == EncryptPassword(password))
            {
                return true;
            }

            return false;
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

        public string EncryptPassword(string password)
        {

        
            using (SHA256 sha256 = SHA256.Create()) 
            {

                byte[] bytes = Encoding.UTF8.GetBytes(password);

         
                byte[] hash = sha256.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }

        public User GetUserById(int id)
        {
            return _Context.Users.Where(u => u.Id == id).FirstOrDefault();
        } 

        public User GetUserByUsername(string username)
        {
            return _Context.Users.Where(u => u.Username == username).FirstOrDefault();
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


        public bool UserExists(string username)
        {
            return _Context.Users.Any(u => u.Username == username);
        }

    }
}
