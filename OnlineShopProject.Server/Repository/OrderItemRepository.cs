using OnlineShopProject.Server.Data;
using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;

namespace OnlineShopProject.Server.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {

        private readonly DataContext _Context;

        public OrderItemRepository(DataContext context)
        {
            this._Context = context;
        }

        public bool CreateOrderItems(List<OrderItem> orderitem)
        {
            _Context.AddRange(orderitem);

            return Save();
        }

        public bool DeleteOrderItem(OrderItem orderitem)
        {
            _Context.Remove(orderitem);

            return Save();
        }

        public bool DeleteOrderItems(List<OrderItem> orderitem)
        {
            _Context.RemoveRange(orderitem);

            return Save();
        }

        public OrderItem GetOrderItemById(int id)
        {
            return _Context.OrderItems.Where(oi => oi.Id == id).FirstOrDefault();
        }

        public ICollection<OrderItem> GetOrderItems()
        {
            return _Context.OrderItems.ToList();
        }

        public List<OrderItem> GetOrderItemsOfAnOrder(int id)
        {
            return _Context.OrderItems.Where(oi => oi.OrderId == id).ToList();
        }

        public bool OrderItemExists(int id)
        {
           return _Context.OrderItems.Any(oi => oi.Id == id);
        }

        public bool Save()
        {
            var saved = _Context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateOrderItem(OrderItem orderItem)
        {
            _Context.Update(orderItem);

            return Save();

        }
    }
}
