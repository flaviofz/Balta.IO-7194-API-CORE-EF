using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _7194SHOP.Data;
using _7194SHOP.Models;
using _7194SHOP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace _7194SHOP.Controllers
{
    [Route("v1/users")]
    public class UserController : Controller
    {
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<List<User>>> Get
        (
            [FromServices] DataContext context
        )
        {
            try
            {
                var users = await context.Users
                .AsNoTracking()
                .ToListAsync();

                return users;
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não foi possível buscar os usuários" });
            }
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult<User>> Post
        (
            [FromServices] DataContext context,
            [FromBody] User model
        )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Força o usuário a ser sempre "funcionário"
                model.Role = "employee";

                context.Users.Add(model);
                await context.SaveChangesAsync();

                // Esconde a senha
                model.Passaword = "";

                return model;
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não foi possível inserir o usuário" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<User>> Put
        (
            [FromServices] DataContext context,
            int id,
            [FromBody] User model
        )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != model.Id)
                    return NotFound(new { message = "Usuário não encontrado" });

                context.Entry<User>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();

                // Esconde a senha
                model.Passaword = "";

                return model;
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não foi possível alterar o usuário" });
            }

        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate
        (
        [FromServices] DataContext context,
        [FromBody] User model
        )
        {
            try
            {
                var user = await context.Users
                .AsNoTracking()
                .Where(x => x.Username == model.Username && x.Passaword == model.Passaword)
                .FirstOrDefaultAsync();

                if (user == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                var token = TokenService.GenerateToken(user);

                return new
                {
                    user = user,
                    token = token
                };
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não foi possível alterar o usuário" });
            }
        }

        // [HttpGet]
        // [Route("anonimo")]
        // [AllowAnonymous]
        // public string Anonimo() => "Anonimo";

        // [HttpGet]
        // [Route("autenticado")]
        // [Authorize]
        // public string Autenticado() => "Autenticado";

        // [HttpGet]
        // [Authorize(Roles = "employee")]
        // [Route("funcionario")]
        // public string Funcionario() => "Funcionario";

        // [HttpGet]
        // [Route("gerente")]
        // [Authorize(Roles = "manager")]
        // public string Gerente() => "Gerente";
    }
}