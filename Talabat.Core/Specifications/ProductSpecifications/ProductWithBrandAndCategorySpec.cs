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
        public ProductWithBrandAndCategorySpec() :base() 
        {
            AddIncludes();
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
