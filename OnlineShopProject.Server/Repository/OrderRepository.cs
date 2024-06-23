using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;
using OnlineShopProject.Server.Data;

namespace OnlineShopProject.Server.Repository
{
    public class OrderRepository : IOrderRepository
    {

        private readonly DataContext _Context;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderRepository(DataContext dataContext, IOrderItemRepository orderItemRepository)
        {

            _Context = dataContext;
            _orderItemRepository = orderItemRepository;
        }

        public bool CreateOrder(Order order)
        {
            _Context.Add(order);

            return Save();
        }

        public bool DeleteOrder(Order order)
        {

            List<OrderItem> itemstodelete = _orderItemRepository.GetOrderItemsOfAnOrder(order.Id);

            _Context.RemoveRange(itemstodelete);

            _Context.Remove(order);


            return Save();
        }

        public bool DeleteOrdersOfUser(int userid)
        {

            List<Order> OrdersToDelete = _Context.Orders.Where(o => o.UserId == userid).ToList();


            foreach (Order Order in OrdersToDelete)
            {
                
                List<OrderItem> itemsToDelete = _Context.OrderItems.Where(oi => oi.OrderId == Order.Id).ToList();

          
                _Context.OrderItems.RemoveRange(itemsToDelete);
            }

            _Context.RemoveRange(OrdersToDelete);

            return Save();
        }

        public Order GetOrderById(int id)
        {
            return _Context.Orders.Where(u => u.Id == id).FirstOrDefault();
        }
         
        public ICollection<Order> GetOrders()
        {
            return _Context.Orders.ToList();
        }

        public bool OrderExists(int id)
        {
            return _Context.Orders.Any(u => u.Id == id);
        }

        public bool Save()
        {
            var saved = _Context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateOrder(Order order)
        {
            _Context.Update(order);

            return Save();
        }
    }
}
