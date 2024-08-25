namespace Talabat.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IGenericRepository<Product> ProductsRepo { get; set; }
        public IGenericRepository<ProductBrand> BrandsRepo { get; set; }
        public IGenericRepository<ProductCategory> CategoriesRepo { get; set; }
        public IGenericRepository<DeliveryMethod> DeliveryMethodsRepo { get; set; }
        public IGenericRepository<OrderItem> OrderItemsRepo { get; set; }
        public IGenericRepository<Order> OrdersRepo { get; set; }

        Task<int> CompleteAsync();
    }
}
