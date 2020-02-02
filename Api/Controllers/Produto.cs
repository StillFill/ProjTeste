using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("teste")]
    public class Produto : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Teste");
        }
    }
}
