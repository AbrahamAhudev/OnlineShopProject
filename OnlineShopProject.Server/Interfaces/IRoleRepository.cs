using OnlineShopProject.Server.Models;

namespace OnlineShopProject.Server.Interfaces
{
    public interface IRoleRepository
    {
        bool Save();

        bool RoleExists(int RoleId);

        UserRole GetUserRole(int UserId);

        bool CreateUserRole(UserRole userrole);

        List<UserRole> GetUserRoles();

        Role GetRole(int RoleId);

        bool DeleteUserRole(int UserId);
    }
}
