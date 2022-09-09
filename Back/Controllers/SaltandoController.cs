using IronBarCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Salt_QR.Interfaces;
using Saltarin.VM;

namespace Saltarin.Controllers
{
    [Route("api")]
    [ApiController]
    public class SaltandoController : ControllerBase
    {

        [HttpPost]
        [Route("MandarUrl")]
        public IActionResult mandarUrl([FromBody] UrlVM url, [FromServices] IQR_SaltService servicio)
        {
            servicio.MandaUrl(url);
            return Ok();
        }

        [HttpPost]
        [Route("VerificarUrl")]
        public IActionResult VerificarUrl([FromBody] UrlVM url, [FromServices] IQR_SaltService servicio)
        {
            var response = servicio.VerificarUrl(url);
            return Ok(response);
            
            
        }


    }
}
