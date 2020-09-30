using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _7194SHOP.Data;
using _7194SHOP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _7194SHOP.Controllers
{
    [Route("products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get
        (
            [FromServices] DataContext context
        )
        {
            var products = await context.Products
                .Include(x => x.Category)
                .AsNoTracking()
                .ToListAsync();

            return products;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> Get
        (
            [FromServices] DataContext context,
            int id
        )
        {
            var product = await context.Products
                .Include(x => x.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return product;
        }

        [HttpGet]
        [Route("categories/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetByCategory
        (
            [FromServices] DataContext context,
            int id
        )
        {
            var products = await context.Products
                .Include(x => x.Category)
                .AsNoTracking()
                .Where(x => x.CategoryId == id)
                .ToListAsync();

            return products;
        }

        // [HttpPost]
        // [Route("")]
        // public async Task<ActionResult<Product>> Post
        // (
        //     [FromServices] DataContext context,
        //     [FromBody] Product model
        // )
        // {

        // }
    }
}