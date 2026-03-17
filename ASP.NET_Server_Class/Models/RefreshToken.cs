using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Server_Class.Models
{
    public class RefreshToken
    {
        [Key]
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate {  get; set; }

    }
}
