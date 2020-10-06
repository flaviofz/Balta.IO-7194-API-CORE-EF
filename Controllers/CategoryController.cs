using System.Collections.Generic;
using System.Threading.Tasks;
using _7194SHOP.Data;
using _7194SHOP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _7194SHOP.Controllers
{
    [Route("v1/categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)]
        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] // Retira o cache do método
        public async Task<ActionResult<List<Category>>> Get
        (
            [FromServices] DataContext context
        )
        {
            try
            {
                var categories = await context.Categories.AsNoTracking().ToListAsync();

                return Ok(categories);
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não foi possível obter as categorias" });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Category>> GetById
        (
            int id,
            [FromServices] DataContext context
        )
        {
            try
            {
                var category = await context.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound(new { message = "Categoria não encontrada" });

                return Ok(category);
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não foi possível obter a categoria" });
            }
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "employee")]
        public async Task<ActionResult<Category>> Post
        (
            [FromBody] Category model,
            [FromServices] DataContext context
        )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                context.Categories.Add(model);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível inserir a categoria" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "employee")]
        public async Task<ActionResult<Category>> Put
        (
            int id,
            [FromBody] Category model,
            [FromServices] DataContext context
        )
        {
            try
            {
                if (model.Id != id)
                    return NotFound(new { message = "Categoria não encontrada" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                context.Entry<Category>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar a categoria" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "employee")]
        public async Task<ActionResult<Category>> Delete
        (
            int id,
            [FromServices] DataContext context
        )
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return NotFound(new { message = "Categoria não encontrada" });

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(new { message = "Categoria removida com sucesso" });
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não foi possível deletar a categoria" });
            }
        }
    }
}