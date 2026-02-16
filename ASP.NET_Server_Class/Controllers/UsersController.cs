using ASP.NET_Server_Class.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace ASP.NET_Server_Class.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService users = new UserService();
        [HttpGet]
        public ActionResult<List<User>> GetUsers() {
            return Ok(users.GetAll());
        }
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id) {
            User? user = users.GetUserById(id);
            if (user is null)
                return NotFound("User not found:(");

            return Ok(users.GetUserById(id));
        }
        [HttpPost]
        public ActionResult PostAddUser(User user)
        {
            int id = users.Add(user);
            user.Id = id;
            return Ok(user);
        }
        [HttpPut]
        public ActionResult PutUpdateUser(User user)
        {
            if (users.Update(user))
                return Ok(user);
            else
                return BadRequest("User not found:(");
        }
        [HttpDelete]
        public ActionResult DeleteDeleteUser(int id)
        {
            User? foundUser = users.GetUserById(id);
            if(foundUser is null)
                return BadRequest("User not found:(");
            if (users.Delete(foundUser))
                return Ok(foundUser);
            else
                return BadRequest("User not found:(");
        }
    }
}
