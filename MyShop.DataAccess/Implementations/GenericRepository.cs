using Microsoft.EntityFrameworkCore;
using MyShop.DataAccess.Data;
using MyShop.Entities.Repositories;


namespace MyShop.DataAccess.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext context;
        private DbSet<T> dbSet;
        public GenericRepository(AppDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null, string? include = null)
        {
            IQueryable<T> query = dbSet.AsQueryable();

           if(predicate != null)
            {
                query = query.Where(predicate);
            }

           if(include != null && !string.IsNullOrWhiteSpace(include))
            {
                // context.Categories.Include("Users,Logos,Products").Tolist();
                foreach(var entity in include.Split(","))
                {
                    query = query.Include(entity);
                }
            }

           return query.ToList();
        }

        public T GetFirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null, string? include = null)
        {
            IQueryable<T> query = dbSet.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (include != null && !string.IsNullOrWhiteSpace(include))
            {
                foreach (var entity in include.Split(","))
                {
                    query = query.Include(entity);
                }
            }

            return query.SingleOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
