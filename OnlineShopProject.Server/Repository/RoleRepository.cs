using OnlineShopProject.Server.Data;
using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;

namespace OnlineShopProject.Server.Repository
{
    public class RoleRepository : IRoleRepository
    {

        private readonly DataContext _Context;

        public RoleRepository(DataContext context)
        {
            _Context = context;
        }

        public bool CreateUserRole(UserRole userrole)
        {
            _Context.UserRoles.Add(userrole);

            return Save(); 
        }

        public bool DeleteUserRole(int UserId)
        {
           UserRole UserRole = _Context.UserRoles.FirstOrDefault(ur => ur.UserId == UserId);

           _Context.UserRoles.Remove(UserRole);

            return Save();
        }

        public Role GetRole(int RoleId)
        {
            return _Context.Roles.FirstOrDefault(r => r.Id == RoleId);
        }

        public UserRole GetUserRole(int UserId)
        {
            return _Context.UserRoles.Where(ur => ur.UserId == UserId).FirstOrDefault();
        }

        public List<UserRole> GetUserRoles()
        {
            return _Context.UserRoles.ToList();
        }

        public bool RoleExists(int RoleId)
        {
            return _Context.Roles.Any(r => r.Id == RoleId);
        }

        public bool Save()
        {
            var saved = _Context.SaveChanges();

            return saved > 0;
        }
    }
}
