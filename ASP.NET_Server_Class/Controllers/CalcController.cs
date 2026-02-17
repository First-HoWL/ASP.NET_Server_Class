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
        public ActionResult PostSum(int[] a)
        {
            int b = 0;
            for(int i = 0; i < a.Length; i++)
            {
                b += a[i];
            }
            return Ok(b);
        }
        [HttpPost("/sub")]
        public ActionResult PostSub(int[] a)
        {
            int b = 0;
            for (int i = 0; i < a.Length; i++)
            {
                b -= a[i];
            }
            return Ok(b);
        }
        [HttpPost("/div")]
        public ActionResult PostDiv(int[] a)
        {
            int b = a[0];
           
            for (int i = 1; i < a.Length; i++)
            {
                b /= a[i];
            }
            return Ok(b);
        }
        [HttpPost("/mul")]
        public ActionResult PostMul(int[] a)
        {
            int b = 1;
            for (int i = 0; i < a.Length; i++)
            {
                b *= a[i];
            }
            return Ok(b);
        }

    }
}
