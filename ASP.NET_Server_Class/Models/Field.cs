using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace ASP.NET_Server_Class.Models
{
    public class Field
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserFieldJson { get; set; }
        public string FullFieldJson {  get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int Mines {  get; set; }
        
    }
    public class AnswerField()
    {
        public string[][] field {  get; set; }
        public bool isLose { get; set; }
        public bool isWin { get; set; }
    }
}
