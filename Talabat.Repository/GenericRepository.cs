namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IReadOnlyList <T>) await _dbContext.Set<Product>()
                    .Include(p=>p.Brand)
                    .Include(p=>p.Category)
                    .ToListAsync();
            }
             return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            if (typeof(T) == typeof(Product))
            {
                return await _dbContext.Set<Product>().Where(p=>p.Id == id).Include(p=>p.Brand).Include(p=>p.Category).FirstOrDefaultAsync() as T;
            }
            return await _dbContext.Set<T>().FindAsync(id);

        }
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpec(spec).ToListAsync();
        }

        public async Task<T?> GetByIdWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpec(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpec(ISpecifications<T> spec)
        {
            return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }

        public async Task<int> GetCountAsync(ISpecifications<T> spec)
        {
            return await ApplySpec(spec).CountAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
        }
    }
}
