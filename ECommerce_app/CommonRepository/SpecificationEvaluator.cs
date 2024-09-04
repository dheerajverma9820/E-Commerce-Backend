using Microsoft.EntityFrameworkCore;

namespace ECommerce_app.CommonRepository
{
    public class SpecificationEvaluator<TEntity> where TEntity : class
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            if (spec.IsPagingEnabled && spec.Skip.HasValue && spec.Take.HasValue)
            {
                query = query.Skip(spec.Skip.Value).Take(spec.Take.Value);
            }

            return query;
        }
    }
}
    
