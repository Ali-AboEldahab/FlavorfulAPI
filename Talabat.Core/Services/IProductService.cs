namespace Talabat.Core.Services
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams specParams);
        Task<Product?> GetProductByIdAsync(int productId);
        Task<int> GetCountAsync(ProductSpecParams specParams);
        Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();
        Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync();
    }
}
