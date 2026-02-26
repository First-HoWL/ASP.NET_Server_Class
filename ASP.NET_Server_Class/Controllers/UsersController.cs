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
        [HttpGet("paged/{page}")]
        public ActionResult<List<User>> GetUsersPages(int page, int size = 2)
        {
            var users = _userService.GetAll();

            if (page > users.Count / size || page < 1 )
                return BadRequest();

            if (size > users.Count / size || size < 1)
                return BadRequest();

            return Ok(new PagedResult<User>() { 
                Items = users.GetRange((page - 1) * size, size),
                TotalCount = users.Count,
                Page = page,
                PagesCount = users.Count / size,
                PageSize = size

            });
        } 
    }
}
