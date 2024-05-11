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

            // Crear una instancia de SHA256, que es un algoritmo de hash
            using (SHA256 sha256 = SHA256.Create()) 
            {
                // Convertir la contraseña en una secuencia de bytes utilizando UTF-8
                byte[] bytes = Encoding.UTF8.GetBytes(password);

                // Calcular el hash de la contraseña
                byte[] hash = sha256.ComputeHash(bytes);

                // Convertir el hash en una cadena Base64 antes de devolverla
                return Convert.ToBase64String(hash);
            }
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
