namespace Talabat.Core.Specifications.ProductSpecifications
{
    public class ProductWithFilterationForCountSpecifications : BaseSpecifications<Product>
    {
        public ProductWithFilterationForCountSpecifications(ProductSpecParams specParams)
            :base(p =>
                       (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search)) &&
                       (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId.Value) &&
                       (!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId.Value)
            )
        {  
        }
    }
}
 