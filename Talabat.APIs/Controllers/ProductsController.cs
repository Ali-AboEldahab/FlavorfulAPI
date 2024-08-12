using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.IRepository;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecifications;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<ProductCategory> _categoriesRepo;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> brandsRepo,
            IGenericRepository<ProductCategory> categoriesRepo,
            IMapper mapper
            )
        {
            _productRepo = productRepo;
            _brandsRepo = brandsRepo;
            _categoriesRepo = categoriesRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productSpecParams)
        {
            var spec = new ProductWithBrandAndCategorySpec(productSpecParams);
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var countSpec = new ProductWithFilterationForCountSpecifications(productSpecParams);
            var count = await _productRepo.GetCountAsync(countSpec);
            return Ok(new Pagination<ProductToReturnDto>(productSpecParams.PageIndex, productSpecParams.PageSize, count, data));
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndCategorySpec(id); 
            var product = await _productRepo.GetByIdWithSpecAsync(spec);
            if(product == null)
                return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandsRepo.GetAllAsync(); 
            return Ok(brands);
        }

        [HttpGet("Categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategoreis()
        {
            var categories = await _categoriesRepo.GetAllAsync();
            return Ok(categories);
        }
    }
}
