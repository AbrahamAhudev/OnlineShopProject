using Microsoft.EntityFrameworkCore;
using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;
using OnlineShopProject.Server.Data;

namespace OnlineShopProject.Server.Repository
{
    public class ProductRepository : IProductRepository
    {



        private readonly DataContext _Context;


        public ProductRepository(DataContext context)
        {
            _Context = context;
        }

        public bool CreateProduct(Product Product)
        {
            _Context.Add(Product);

            return Save();
        }

        public bool DeleteProduct(Product Product)
        {
            _Context.Remove(Product);

            return Save();
        }

        public Product GetProductById(int id)
        {
            return _Context.Products.Where(u => u.Id == id).FirstOrDefault();
        }

        public ICollection<Product> GetProducts()
        {
            return _Context.Products.ToList();
        }

        public bool ProductExists(int id)
        {
            return _Context.Products.Any(u => u.Id == id);
        }

        public bool Save()
        {
            var saved = _Context.SaveChanges();

            return saved > 0;
        }

        public ICollection<Product> SearchProducts(string SearchString)
        {
            return _Context.Products
                .Where(p => p.Name.Contains(SearchString))
                .ToList();
        }

        public bool UpdateProduct(Product Product)
        {
            _Context.Update(Product);

            return Save();
        }
    }
}
