using Microsoft.AspNetCore.Mvc;

namespace _7194SHOP.Controllers
{
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        //[HttpGet]
        [Route("")]
        public string MeuMetodo()
        {
            return "Meu método";
        }
    }
}