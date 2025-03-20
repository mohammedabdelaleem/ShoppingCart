

using MyShop.DataAccess.Data;
using MyShop.Entities.Models;
using MyShop.Entities.Repositories;

namespace MyShop.DataAccess.Implementations
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Product product)
        {
            var productDB = context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (productDB != null)
            {
                productDB.Name = product.Name;
                productDB.Description = product.Description;
                productDB.Price = product.Price;
                productDB.CategoryId = product.CategoryId;
                productDB.Img = product.Img;
            }

        }
    }
}
