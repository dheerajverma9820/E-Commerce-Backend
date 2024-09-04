using System.Linq.Expressions;

namespace ECommerce_app.Generic_Interface
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(int id);

       
    }
}
