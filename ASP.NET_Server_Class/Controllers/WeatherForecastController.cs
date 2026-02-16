using ASP.NET_Server_Class.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Server_Class.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        //private UserService users = new UserService();

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        //[HttpGet("/test")]
        //public ActionResult GetTestValue()
        //{
        //    return Ok("Test value");
        //}
        //[HttpGet("/user/{id?}")]
        //public ActionResult GetUserById(int? id)
        //{

        //    return Ok($"Getting user with id {id}");
        //}
        //[HttpGet("/users")]
        //public ActionResult<List<User>> GetUsers(){
        //    return Ok(users.GetAll());
        //}
    }
}
