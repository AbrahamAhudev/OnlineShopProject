using OnlineShopProject.Server.Models;

namespace OnlineShopProject.Server.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<Order> GetOrders();

        Order GetOrderById(int id);

        bool OrderExists(int id);

        bool CreateOrder(Order order);

        bool UpdateOrder(Order order);

        bool DeleteOrder(Order order);

        bool DeleteOrdersOfUser(int userid);

        bool Save();
    }
}
