using _7194SHOP.Models;
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
        public Category Post
        (
            [FromBody] Category model
        )
        {
            return model;
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