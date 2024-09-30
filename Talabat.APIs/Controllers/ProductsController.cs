namespace Talabat.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productSpecParams)
        {
            IReadOnlyList<Core.Entities.Product> products = await _productService.GetProductsAsync(productSpecParams);
            IReadOnlyList<ProductToReturnDto> data = _mapper.Map<IReadOnlyList<Core.Entities.Product>, IReadOnlyList<ProductToReturnDto>>(products);
            int count = await _productService.GetCountAsync(productSpecParams);
            return Ok(new Pagination<ProductToReturnDto>(productSpecParams.PageIndex, productSpecParams.PageSize, count, data));
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        { 
            Core.Entities.Product? product = await _productService.GetProductByIdAsync(id);
            if(product == null)
                return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Core.Entities.Product,ProductToReturnDto>(product));
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
            => Ok(await _productService.GetBrandsAsync());

        [HttpGet("Categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategoreis()
            => Ok(await _productService.GetCategoriesAsync());
    }
}
