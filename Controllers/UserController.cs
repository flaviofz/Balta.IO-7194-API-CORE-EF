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
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
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

                context.Users.Add(model);
                await context.SaveChangesAsync();

                return model;
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não foi possível inserir o usuário" });
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