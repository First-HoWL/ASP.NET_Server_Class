namespace ASP.NET_Server_Class
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateOnly Birthday { get; set; }
        public string Gender { get; set; }
        public User()
        {}
        public User(int id, UserDTO userData)
        {
            Id = id;
            Name = userData.Name;
            Email = userData.Email;
            Birthday = userData.Birthday;
            Gender = userData.Gender;
        }
        
    }
    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateOnly Birthday { get; set; }
        public string Gender { get; set; }
    }
}
