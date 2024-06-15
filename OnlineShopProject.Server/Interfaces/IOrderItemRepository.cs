using OnlineShopProject.Server.Models;

namespace OnlineShopProject.Server.Interfaces
{
    public interface IOrderItemRepository
    {

        ICollection<OrderItem> GetOrderItems();

        OrderItem GetOrderItemById(int id);

        List<OrderItem> GetOrderItemsOfAnOrder(int id);

        bool OrderItemExists(int id);

        bool CreateOrderItems(List<OrderItem> orderitem);

        bool UpdateOrderItem(OrderItem orderitem);

        bool DeleteOrderItem(OrderItem orderitem);

        bool DeleteOrderItems(List<OrderItem> orderitem);

        bool Save();
    }
}
