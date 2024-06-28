using OnlineShopProject.Server.DTOs;
using OnlineShopProject.Server.Models;


namespace OnlineShopProject.Server.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();

        ICollection<Product> SearchProducts(string SearchString);

        Product GetProductById(int id);

        bool ProductExists(int id);

        bool CreateProduct(Product Product);

        bool UpdateProduct(Product Product);

        bool DeleteProduct(Product Product);

        bool DeleteProducts(List<Product> Products);

        bool Save();
    }
}
