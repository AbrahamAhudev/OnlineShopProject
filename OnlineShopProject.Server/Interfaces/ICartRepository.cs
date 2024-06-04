using OnlineShopProject.Server.Models;

namespace OnlineShopProject.Server.Interfaces
{
    public interface ICartRepository
    {

        Cart GetCart(int id);

        ICollection<Cart> GetCarts();

        bool CartExists(int id);

        bool DeleteCart(Cart cart);

        bool Save();

        bool UpdateCart(Cart cart);
    }
}
