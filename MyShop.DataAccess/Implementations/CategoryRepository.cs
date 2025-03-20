using MyShop.DataAccess.Data;
using MyShop.Entities.Models;
using MyShop.Entities.Repositories;

namespace MyShop.DataAccess.Implementations
{
    public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
    {
        private readonly AppDbContext context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Category category)
        {
            var categoryInDB = context.Categories.FirstOrDefault(c => c.Id == category.Id);
            
            if (categoryInDB != null)
            {
                categoryInDB.Name = category.Name;
                categoryInDB.Description = category.Description;
            }
        
        }
    }
}
