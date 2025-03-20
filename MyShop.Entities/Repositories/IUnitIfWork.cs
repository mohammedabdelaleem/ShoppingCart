using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.Repositories
{
    public interface IUnitIfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        int Complete(); // save changes
    }
}
