namespace Talabat.Core.Specifications.ProductSpecifications
{
    public class ProductWithBrandAndCategorySpec :BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategorySpec(ProductSpecParams productSpecParams)
            :base( p =>
                        (string.IsNullOrEmpty(productSpecParams.Search) || p.Name.ToLower().Contains(productSpecParams.Search)) &&
                        (!productSpecParams.BrandId.HasValue || p.BrandId == productSpecParams.BrandId.Value) && 
                        (!productSpecParams.CategoryId.HasValue || p.CategoryId == productSpecParams.CategoryId.Value))
             
        {
            AddIncludes();

            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort)   
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                    break;

                    case "PriceDesc":
                        AddOrderByDesc(p => p.Price);
                    break;

                    default:
                        AddOrderBy(p=>p.Name);
                    break;
                }
            }
            else
                AddOrderBy(p => p.Name);

            //Pagination
            ApplyPagination((productSpecParams.PageIndex - 1) * productSpecParams.PageSize , productSpecParams.PageSize);
        }

        public ProductWithBrandAndCategorySpec(int id): base(p => p.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }

    }


   
}
