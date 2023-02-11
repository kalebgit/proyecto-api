using api.ADO.NET;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("Producto/{idUsuario}")]
        public void GetProduct(long userId)
        {
            ProductHandler.GetProduct(userId);
        }

        [HttpPost("Producto")]
        public void CreateProduct([FromBody]Product product)
        {
            ProductHandler.AddProducts(product);
        }

        [HttpPut("Producto")]
        public void UpdateProduct([FromBody]Product product)
        {
            ProductHandler.UpdateProduct(product);
        }


        [HttpDelete("Producto/{id}")]
        public void DeleteProduct(long id)
        {
            ProductHandler.DeleteProduct(id);
        }
    }
}
