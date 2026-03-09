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
        public void Register(User user) {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            user.PasswordSalt = salt;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash, salt);
            Add(user);

        }
        public User? ValidateUser(UserLoginDTO credentials)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == credentials.Email);
            if (user == null) {
                return null;
            }
            if (!BCrypt.Net.BCrypt.Verify(credentials.Password, user.PasswordHash))
            {
                return null;
            }

            return user;
        }
    }
}

