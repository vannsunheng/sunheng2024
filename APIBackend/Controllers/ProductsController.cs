using APIBackend.DTOS;
using APIBackend.Error;
using APIBackend.Helper;
using AutoMapper;
using Core.Entities;
using Core.Interface;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;

namespace APIBackend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseAPIController
    {
        public IGenericRepository<Product> Productrepo { get; }
        public IGenericRepository<ProductBrand> ProductBrandRepo { get; }
        public IGenericRepository<ProductType> ProductTypeRepo { get; }
        public IMapper Mapper { get; }
        public ProductsController(IGenericRepository<Product> productrepo, IGenericRepository<ProductBrand> ProductBrandRepo,

        IGenericRepository<ProductType> ProductTypeRepo,IMapper mapper)
        {
            this.Mapper = mapper;
            this.ProductTypeRepo = ProductTypeRepo;
            this.ProductBrandRepo = ProductBrandRepo;
            this.Productrepo = productrepo;

        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetProducts([FromQuery] ProductSpecParams ProductParams)
        {
            var spec =new ProductsWithTypesandBrandsSpecification(ProductParams);

            var countspec=new ProductWithFiltersForCountSpecification(ProductParams);
            var totalItem=await Productrepo.CountAsync(countspec);

            var products = await Productrepo.ListAsynce(spec);
            var data=Mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDTO>>(products);

            return Ok(new Pagination<ProductToReturnDTO>(ProductParams.PageIndex,totalItem,ProductParams.PageSize,data));  
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponce),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            var spec =new ProductsWithTypesandBrandsSpecification(id);
            var products = await Productrepo.GetEntitywithSpec(spec);
            if (products == null) return NotFound(new APIResponce(404));
            return Ok(Mapper.Map<Product,ProductToReturnDTO>(products));
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await ProductBrandRepo.ListAllAsync();
            return Ok(productBrands);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductTypes()
        {
            var productTypes = await ProductTypeRepo.ListAllAsync();
            return Ok(productTypes);
        }
    }
}