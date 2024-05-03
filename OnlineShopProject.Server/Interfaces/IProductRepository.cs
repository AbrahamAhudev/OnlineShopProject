using OnlineShopProject.Server.Models;


namespace OnlineShopProject.Server.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();

        Product GetProductById(int id);

        bool ProductExists(int id);

        bool CreateProduct(Product Product);

        bool UpdateProduct(Product Product);

        bool DeleteProduct(Product Product);

        bool Save();
    }
}
