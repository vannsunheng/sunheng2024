using APIBackend.DTOS;
using APIBackend.Error;
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
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProducts()
        {
            var spec =new ProductsWithTypesandBrandsSpecification();
            var products = await Productrepo.ListAsynce(spec);

            return Ok(Mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDTO>>(products));  
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