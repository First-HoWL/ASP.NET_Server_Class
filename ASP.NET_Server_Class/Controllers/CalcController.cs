using ASP.NET_Server_Class.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace ASP.NET_Server_Class.Controllers
{
    [ApiController]
    [Route("calc/[controller]")]
    public class CalcController : ControllerBase
    {
        [HttpPost("/sum")]
        public ActionResult PostSum(int a, int b)
        {
            return Ok(a + b);
        }
        [HttpPost("/sub")]
        public ActionResult PostSub(int a, int b)
        {
            return Ok(a - b);
        }
        [HttpPost("/div")]
        public ActionResult PostDiv(int a, int b)
        {
            return Ok(a / b);
        }
        [HttpPost("/mul")]
        public ActionResult PostMul(int a, int b)
        {
            return Ok(a * b);
        }

    }
}
