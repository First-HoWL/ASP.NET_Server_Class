using ASP.NET_Server_Class.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace ASP.NET_Server_Class.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _users = new UserService();
        [HttpGet]
        public ActionResult<List<User>> GetUsers() {
            return Ok(_users.GetAll());
        }
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id) {
            User? user = _users.GetUserById(id);
            if (user is null)
                return NotFound("User not found:(");

            return Ok(_users.GetUserById(id));
        }
        [HttpPost]
        public ActionResult PostAddUser(UserDTO user)
        {
            return Ok(_users.Add(user));
        }
        [HttpPut]
        public ActionResult PutUpdateUser(User user)
        {
            if (_users.Update(user))
                return Ok(user);
            else
                return BadRequest("User not found:(");
        }
        [HttpDelete]
        public ActionResult DeleteDeleteUser(int id)
        {
            User? foundUser = _users.GetUserById(id);
            if(foundUser is null)
                return BadRequest("User not found:(");
            if (_users.Delete(foundUser))
                return Ok(foundUser);
            else
                return BadRequest("User not found:(");
        }
    }
}
