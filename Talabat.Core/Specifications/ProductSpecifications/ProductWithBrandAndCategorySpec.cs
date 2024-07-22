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
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }

        public ProductWithBrandAndCategorySpec(int id): base(p => p.Id == id)
        {
            
        }
    }


   
}
