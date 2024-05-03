using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;
using OnlineShopProject.Server.Data;

namespace OnlineShopProject.Server.Repository
{
    public class OrderRepository : IOrderRepository
    {

        private readonly DataContext _Context;

        public OrderRepository(DataContext dataContext)
        {

            _Context = dataContext;

        }

        public bool CreateOrder(Order order)
        {
            _Context.Add(order);

            return Save();
        }

        public bool DeleteOrder(Order order)
        {
            _Context.Remove(order);

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
