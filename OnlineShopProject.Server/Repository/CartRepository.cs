using Microsoft.EntityFrameworkCore;
using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;
using OnlineShopProject.Server.Data;

namespace OnlineShopProject.Server.Repository
{
    public class CartRepository : ICartRepository
    {


        private readonly DataContext _Context;

        public CartRepository(DataContext context)
        {
            this._Context = context;
        }

        public bool CartExists(int id)
        {
            return _Context.Carts.Any(c => c.Id == id);
        }

        public bool DeleteCart(Cart cart)
        {
            _Context.Remove(cart);


            List<CartItem> ItemsToDelete = _Context.CartItems.Where(ci => ci.CartId == cart.Id).ToList();

            _Context.RemoveRange(ItemsToDelete);

            return Save();
        }

        public Cart GetCart(int id)
        {
            return _Context.Carts.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Cart> GetCarts()
        {
            return _Context.Carts.ToList();
        }

        public bool Save()
        {
            var saved = _Context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateCart(Cart cart)
        {
            _Context.Update(cart);

            return Save();
        }
    }
}
