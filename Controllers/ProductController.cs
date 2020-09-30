using System.Collections.Generic;
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