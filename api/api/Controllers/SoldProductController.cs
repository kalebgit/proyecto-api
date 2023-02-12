using api.ADO.NET;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class SoldProductController : ControllerBase
    {
        [HttpGet("ProductoVendido/{idUsuario}")]
        public List<SoldProduct> GetSoldProducts(long idUsuario)
        {
            return SoldProductHandler.GetSoldProducts(idUsuario);
        }
    }
}
