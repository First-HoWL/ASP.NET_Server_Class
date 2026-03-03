using ASP.NET_Server_Class.Models;
using ASP.NET_Server_Class.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;


namespace ASP.NET_Server_Class.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FieldsControler : ControllerBase
    {
        private readonly FieldService _fieldService;
        public FieldsControler(FieldService fieldService)
        {
            _fieldService = fieldService;
        }
        [HttpGet("fields")]
        public ActionResult<List<Field>> GetFields()
        {
            return Ok(_fieldService.GetAll());
        }
        [HttpPost("CreateField")]
        public ActionResult<Field> CreateField( int SizeX = 10,  int SizeY = 10,  int Mines = 10)
        {
            string[][] map = new string[SizeY][];

            for (int i = 0; i < SizeY; i++)
            {
                map[i] = new string[SizeX];
                for (int j = 0; j < SizeX; j++)
                {
                    map[i][j] = "";
                }
            }
            string[][] fullMap = _fieldService.Generate(SizeX, SizeY, Mines);



            Field field = new Field() { 
                FullFieldJson = JsonSerializer.Serialize(fullMap),
                UserFieldJson = JsonSerializer.Serialize(map),
                SizeX = SizeX,
                SizeY = SizeY,
                Mines = Mines
            };

            _fieldService.Add(field);

            return Ok(field);
        }
        [HttpGet("fields/{id}")]
        public ActionResult<Field> GetField(int id) {
            return Ok(_fieldService.GetAll().Where(i => i.Id == id).FirstOrDefault());
        }

    }
}
