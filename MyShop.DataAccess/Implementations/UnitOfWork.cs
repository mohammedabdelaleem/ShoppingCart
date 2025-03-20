using MyShop.DataAccess.Data;
using MyShop.Entities.Repositories;

namespace MyShop.DataAccess.Implementations
{
    public class UnitOfWork : IUnitIfWork
    {
        private readonly AppDbContext context;

        public ICategoryRepository Category  {get; private set;}

        public IProductRepository Product { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
            Category = new CategoryRepository(context);
            Product = new ProductRepository(context);
        }
        public int Complete()
        {
           return context.SaveChanges();
        }

        public void Dispose()
        {
             context.Dispose();
        }
    }
}
