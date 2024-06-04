using OnlineShopProject.Server.Data;
using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;

namespace OnlineShopProject.Server.Repository
{
    public class CartItemRepository : ICartItemRepository
    {

        private readonly DataContext _Context;

        public CartItemRepository(DataContext context)
        {
            this._Context = context;
        }

        public bool CartItemExists(int id)
        {
            return _Context.CartItems.Any(c => c.Id == id);
        }

        public bool DeleteCartItem(CartItem CartItem)
        {
            _Context.Remove(CartItem);

            return Save();
        }

        public bool DeleteCartItems(List<CartItem> CartItems)
        {
            _Context.RemoveRange(CartItems);

            return Save();
        }

        public CartItem GetCartItem(int id)
        {
            return _Context.CartItems.Where(ci => ci.Id == id).FirstOrDefault();
        }

        public ICollection<CartItem> GetCartItems()
        {
            return _Context.CartItems.ToList();
        }

        public bool Save()
        {
            var saved = _Context.SaveChanges();

            return saved > 0;
        }
    }
}
