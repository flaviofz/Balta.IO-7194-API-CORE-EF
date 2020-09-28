using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _7194SHOP.Data;
using _7194SHOP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _7194SHOP.Controllers
{
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get()
        {
            return new List<Category>();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            return new Category();
        }

        [HttpPost]
        [Route("")]
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