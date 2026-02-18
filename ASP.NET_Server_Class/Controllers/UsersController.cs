using ASP.NET_Server_Class.Services;
using ASP.NET_Server_Class.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Server_Class.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        public UsersController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return Ok(_userService.GetAll());
        }
        [HttpPost]
        public ActionResult AddUser([FromBody]User user) 
        {
            _userService.Add(user);
            return Ok();
        }
    }
}
