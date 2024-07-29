using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecifications
{
    public class ProductWithBrandAndCategorySpec :BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategorySpec(string sort) :base() 
        {
            AddIncludes();

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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
