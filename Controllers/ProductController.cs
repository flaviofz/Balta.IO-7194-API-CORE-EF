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
            try
            {
                var products = await context.Products
                .Include(x => x.Category)
                .AsNoTracking()
                .ToListAsync();

                return Ok(products);
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não foi possível obter oss producto" });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> Get
        (
            [FromServices] DataContext context,
            int id
        )
        {
            try
            {
                var product = await context.Products
                .Include(x => x.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                    return NotFound(new { message = "Produto não encontrado" });

                return Ok(product);
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não foi possível obter o producto" });
            }
        }

        [HttpGet]
        [Route("categories/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetByCategory
        (
            [FromServices] DataContext context,
            int id
        )
        {
            try
            {
                var products = await context.Products
                .Include(x => x.Category)
                .AsNoTracking()
                .Where(x => x.CategoryId == id)
                .ToListAsync();

                return Ok(products);
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não foi possível obter os productos" });
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post
        (
            [FromServices] DataContext context,
            [FromBody] Product model
        )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                context.Products.Add(model);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não foi possível inserir o producto" });
            }
        }
    }
}