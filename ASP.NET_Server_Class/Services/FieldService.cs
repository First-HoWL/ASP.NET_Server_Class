using ASP.NET_Server_Class.Models;
using System.Drawing;


namespace ASP.NET_Server_Class.Services
{
    public class FieldService
    {
        public readonly UsersDbContext _context;

        public FieldService(UsersDbContext context)
        {
            _context = context;
        }
        public List<Field> GetAll() => _context.Fields.ToList();

        public void Add(Field Field)
        {
            _context.Fields.Add(Field);
            _context.SaveChanges();
        }

        public string[][] Generate(int SizeX, int SizeY, int Mines)
        {
            Random rnd = new Random();
            string[][] map = new string[SizeY][];
            for (int i = 0; i < SizeY; i++)
            {
                map[i] = new string[SizeX];
                for (int j = 0; j < SizeX; j++)
                {
                    map[i][j] = "O";
                }
            }
            for(int i = 0; i < Mines; i++)
            {
                int y = rnd.Next(0, SizeY);
                int x = rnd.Next(0, SizeX);
                if(map[y][x] != "B")
                {
                    map[y][x] = "B";
                }
                else
                {
                    i--;
                }
            }

            for (int i = 0; i < SizeY; i++)
            {
                for (int j = 0; j < SizeX; j++)
                {
                    if(map[i][j] != "B") { 
                        int mines = 0;
                        if (i >= 1 && j >= 1 && map[i - 1][j - 1] == "B")
                            mines++;
                        if (i >= 1 && map[i - 1][j] == "B")
                            mines++;
                        if (i >= 1 && j < SizeX - 1 && map[i - 1][j + 1] == "B")
                            mines++;
                        if (j >= 1 && map[i][j - 1] == "B")
                            mines++;
                        if (j < SizeX - 1 && map[i][j + 1] == "B")
                            mines++;
                        if (i < SizeY - 1 && j >= 1 && map[i + 1][j - 1] == "B")
                            mines++;
                        if (i < SizeY - 1 && map[i + 1][j] == "B")
                            mines++;
                        if (i < SizeY - 1 && j < SizeX - 1 && map[i + 1][j + 1] == "B")
                            mines++;
                        map[i][j] = mines != 0 ? mines.ToString() : "O";
                    }
                }
            }

            return map;
        }

        string[][] OpenGaps(int SizeX, int SizeY, string[][] UserField, string[][] Field, int x, int y) 
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
            map[y][x] = "O";

            bool stop = false;
            while (!stop)
            {
                stop = true;
                for (int i = 0; i < SizeY; i++)
                {
                    for (int j = 0; j < SizeX; j++)
                    {
                        if (map[i][j] == "O")
                        {
                            
                            for (int k = Math.Max(i - 1, 0); k <= Math.Min(i + 1, SizeY - 1); k++)
                            {
                                for (int l = Math.Max(j - 1, 0); l <= Math.Min(j + 1, SizeX - 1); l++)
                                {
                                    if (UserField[k][l] == "" && map[k][l] == "")
                                    {
                                        stop = false;
                                        map[k][l] = Field[k][l];
                                    }

                                }
                            }

                        }
                    }
                }
            }

            return map;

        }


        public string[][] Move(int SizeX, int SizeY, string[][] UserField, string[][] Field, int x, int y)
        {
            // bool isStop = false;
            string[][] map = new string[SizeY][];

            for (int i = 0; i < SizeY; i++)
            {
                map[i] = new string[SizeX];
                for (int j = 0; j < SizeX; j++)
                {
                    map[i][j] = "";
                }
            }

            if (Field[y][x] == "B")
                map[y][x] = "B";
            else if (Field[y][x] == "O")
            {
                string[][] newmap = OpenGaps(SizeX, SizeY, map, Field, x, y);

                for (int k = 0; k < SizeY; k++)
                {
                    for (int l = 0; l < SizeX; l++)
                    {
                        map[k][l] = newmap[k][l];
                    }
                }

            }
            else 
            {
                map[y][x] = Field[y][x];
            }

            


            return map;
        }





        public void Update(Field Field)
        {
            Field? foundfield = _context.Fields.Where(d => d.Id == Field.Id).FirstOrDefault();
            if (foundfield is null)
            {
            }
            else { 
                foundfield.UserFieldJson = Field.UserFieldJson;
                foundfield.FullFieldJson = Field.FullFieldJson;
                foundfield.SizeX = Field.SizeX;
                foundfield.SizeY = Field.SizeY;
                foundfield.Mines = Field.Mines;
            }
            _context.SaveChanges();

        }
        public bool Delete(int id)
        {
            Field? foundfield = _context.Fields.Where(d => d.Id == id).FirstOrDefault();
            if (foundfield is null)
            {
                return false;
            }
            else
            {
                _context.Fields.Remove(foundfield);
                _context.SaveChanges();
                return true;
            }
            

        }
    }
}
