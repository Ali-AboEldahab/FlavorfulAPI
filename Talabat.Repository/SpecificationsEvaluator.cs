namespace Talabat.Repository
{
    internal class SpecificationsEvaluator <TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
        {
            var query = inputQuery;

            //filter
            if (spec.Criteria is not null) 
                query = query.Where(spec.Criteria);

            //sort
            if(spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);

            else if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);

            //pagination
            if(spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take); 

            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }
    }
}
