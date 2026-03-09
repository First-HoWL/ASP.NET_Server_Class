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
        [HttpPost("move")]
        public ActionResult<Field> PostMove(int id, int x, int y)
        {
            Field? field = _fieldService.GetAll().Where(i => i.Id == id).FirstOrDefault();
            string[][] newField;
            string[][] newField2 = new string[field.SizeY][];
            string[][] fullField = JsonSerializer.Deserialize<string[][]>(field.FullFieldJson);

            bool Islose = false;
            bool iswin = false;

            for (int i = 0; i < field.SizeY; i++)
            {
                newField2[i] = new string[field.SizeX];
                for (int j = 0; j < field.SizeX; j++)
                {
                    newField2[i][j] = "";
                }
            }

            if (field == null) {
                return BadRequest();
            }
            if (x > field.SizeX - 1 || x < 0 || y > field.SizeY - 1 || y < 0)
            {
                return BadRequest();
            }
            else
            {
                newField2 = JsonSerializer.Deserialize<string[][]>(field.UserFieldJson);
                newField = _fieldService.Move(field.SizeX, field.SizeY, newField2, fullField, x, y);

                iswin = true;
                for (int k = 0; k < field.SizeY; k++)
                {
                    for (int l = 0; l < field.SizeX; l++)
                    {
                        if(newField[k][l] != "") { 
                            newField2[k][l] = newField[k][l];
                        }
                        if(newField[k][l] == "B")
                        {
                            Islose = true;
                        }
                        if (newField2[k][l] == "" && fullField[k][l] != "B")
                        {
                            iswin = false;
                        }
                    }
                }
                _fieldService.Update(new Field
                {
                    Id = id,
                    SizeY = field.SizeY,
                    SizeX = field.SizeX,
                    FullFieldJson = field.FullFieldJson,
                    Mines = field.Mines,
                    UserFieldJson = JsonSerializer.Serialize(newField2)
                });

            }
            
            

            return Ok(new AnswerField
            {
                field = Islose ? JsonSerializer.Deserialize<string[][]>(field.FullFieldJson) : newField2,
                isLose = Islose,
                isWin = iswin
            });
        }

    }
}
