using api.ADO.NET;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        [HttpPost("Venta/{idUsuario}")]
        public void ChargeSale(long idUsuario, [FromBody]List<SoldProduct> soldProducts)
        {
            SaleHandler.ChargeSale(idUsuario, soldProducts);
        }
    }
}
