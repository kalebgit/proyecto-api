using api.ADO.NET;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoldProductController : ControllerBase
    {
        [HttpGet("ProductoVendido/{idUsuario}")]
        public void GetSoldProducts(long idUsuario)
        {
            SoldProductHandler.GetSoldProduct(idUsuario);
        }
    }
}
