using Core.Entities;
using Core.Interface;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIBackend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
    
        public IProductRepository repos { get; }
       
        public ProductsController(IProductRepository repository)
        {
            this.repos = repository;
        
        }
       
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(){
            var products= await repos.GetProductsAsync();
    
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id){
            return Ok(await repos.GetProductByIdAsync(id));
        }
         [HttpGet("Brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands(){
            var productBrands= await repos.GetProductBrandsAsync();
            return Ok(productBrands);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductTypes(){
            var productTypes= await repos.GetProductTypesAsync();
            return Ok(productTypes);
        }
    }
    }