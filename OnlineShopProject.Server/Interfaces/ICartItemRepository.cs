using OnlineShopProject.Server.Models;

namespace OnlineShopProject.Server.Interfaces
{
    public interface ICartItemRepository
    {
        bool Save();

     

        CartItem GetCartItem(int id);

        ICollection<CartItem> GetCartItems();

        bool CartItemExists(int id);

        bool DeleteCartItem(CartItem CartItem);

        bool DeleteCartItems(List<CartItem> CartItems);

    }
}
