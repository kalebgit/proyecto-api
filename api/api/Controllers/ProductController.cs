using api.ADO.NET;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("Producto/{id}")]
        public Product GetProduct(long id)
        {
            return ProductHandler.GetProduct(id);
        }

        [HttpGet("Productos")]
        public List<Product> GetProduct()
        {
            return ProductHandler.GetProducts();
        }

        [HttpPost("Producto")]
        public string CreateProduct([FromBody]Product product)
        {
            return ProductHandler.AddProducts(product) == 1 ? "Se ha creado el producto" :
                "No se pudo crear el producto";
        }

        [HttpPut("Producto")]
        public string UpdateProduct([FromBody]Product product)
        {
            return ProductHandler.UpdateProduct(product) == 1 ? "Se ha actualizado del producto" :
                "No se puedo actualizar el producto";
        }


        [HttpDelete("Producto/{id}")]
        public void DeleteProduct(long id)
        {
            ProductHandler.DeleteProduct(id);
        }
    }
}
