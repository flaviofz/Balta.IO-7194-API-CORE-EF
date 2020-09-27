using Microsoft.AspNetCore.Mvc;

namespace _7194SHOP.Controllers
{
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public string Get()
        {
            return "Get";
        }

        [HttpGet]
        [Route("{id:int}")]
        public string GetById(int id)
        {
            return "Get " + id.ToString();
        }

        [HttpPost]
        [Route("")]
        public string Post()
        {
            return "Post";
        }

        [HttpPut]
        [Route("")]
        public string Put()
        {
            return "Put";
        }

        [HttpDelete]
        [Route("")]
        public string Delete()
        {
            return "Delete";
        }
    }
}