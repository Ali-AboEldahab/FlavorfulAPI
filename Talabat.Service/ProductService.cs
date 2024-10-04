namespace Flavorful.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams specParams)
        {
            ProductWithBrandAndCategorySpec spec = new(specParams);
            IReadOnlyList<Product> products = await _unitOfWork.ProductsRepo.GetAllWithSpecAsync(spec);
            return products;
        }
        public async Task<int> GetCountAsync(ProductSpecParams specParams)
        {
            ProductWithFilterationForCountSpecifications countSpec = new(specParams);
            int count = await _unitOfWork.ProductsRepo.GetCountAsync(countSpec);
            return count;
        }
        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            ProductWithBrandAndCategorySpec spec = new(productId);
            Product? product = await _unitOfWork.ProductsRepo.GetByIdWithSpecAsync(spec);
            return product;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
            => await _unitOfWork.BrandsRepo.GetAllAsync();

        public async Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync()
            => await _unitOfWork.CategoriesRepo.GetAllAsync();

    }
}
