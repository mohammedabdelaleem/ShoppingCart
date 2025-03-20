using System.Linq.Expressions;

namespace MyShop.Entities.Repositories
{
    public interface IGenericRepository<T> where T : class
    {

        // context.Categories.ToList();
        // context.Categories.Include("Products").ToList();
        // context.Categories.Where(c=>c.Id==id).ToList();
        IEnumerable<T> GetAll(Expression<Func<T,bool>>? predicate =null, string? include=null);


        // context.Categories.ToSingleOrDefault();
        // context.Categories.Include("Products").ToSingleOrDefault();
        // context.Categories.Where(c=>c.Id==id).ToSingleOrDefault();
        T GetFirstOrDefault(Expression<Func<T, bool>>? predicate = null, string? include = null);

        void Add(T entity);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}
