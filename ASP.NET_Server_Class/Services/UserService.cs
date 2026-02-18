using ASP.NET_Server_Class.Models;

namespace ASP.NET_Server_Class.Services
{
    public class UserService
    {
        public readonly UsersDbContext _context;

        public UserService(UsersDbContext context)
        {
            _context = context;
        }
        public List<User> GetAll() => _context.Users.ToList();

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }


        //List<User> users = new List<User>();
        //int lastId = 0;

        //public UserService()
        //{
        //    Add(new User()
        //    {
        //        Id = 1,
        //        Name = "user 1",
        //        Email = "user1@test.mail.com",
        //        Birthday = DateOnly.Parse("01.04.2001"),
        //        Gender = "male"
        //    });
        //    Add(new User()
        //    {
        //        Id = 2,
        //        Name = "user 2",
        //        Email = "user2@test.mail.com",
        //        Birthday = DateOnly.Parse("24.07.1999"),
        //        Gender = "female"
        //    });
        //}


        //private int GetLastId() => ++lastId;
        //public int Add(User user)
        //{
        //    user.Id = GetLastId();
        //    users.Add(user);
        //    return user.Id;
        //}
        //public int Add(UserDTO user)
        //{
        //    User NewUser = new User(GetLastId(), user);
        //    users.Add(NewUser);
        //    return NewUser.Id;
        //}

        //public User? GetUserById(int Id) =>
        //    users.FirstOrDefault(user => user.Id == Id);
        //public bool Update(User user)
        //{
        //    User? foundUser = GetUserById(user.Id);
        //    if (foundUser is null)
        //    {

        //        return false;

        //    }
        //    foundUser.Name = user.Name;
        //    foundUser.Email = user.Email;
        //    foundUser.Gender = user.Gender;
        //    foundUser.Birthday = user.Birthday;
        //    return false;

        //}
        //public bool Delete(User user)
        //{
        //    User? foundUser = GetUserById(user.Id);
        //    if (foundUser is null)
        //    {
        //        return false;

        //    }
        //    users.Remove(foundUser);
        //    return true;
        //}
    }
}

