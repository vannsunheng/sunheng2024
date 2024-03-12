using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIBackend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
    
        private readonly StoreContext _context;
        
        public ProductsController(StoreContext context)
        {
            this._context = context;
        }
       
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(){
            var products= await _context.products.ToListAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id){
            return Ok(await _context.products.FindAsync(id));
        }
        
    }
    }