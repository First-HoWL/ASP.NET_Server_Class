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
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash, BCrypt.Net.BCrypt.GenerateSalt());
            Add(user);

        }

        public bool Delete(int id)
        {
            User? user = _context.Users.Where(i => i.Id == id).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            else
            {
                _context.Remove(user);
            }
            _context.SaveChanges();
            return true;
        }

        public void Update(User user)
        {
            User? foundUser = _context.Users.Where(d => d.Id == user.Id).FirstOrDefault();
            if (foundUser != null)
            {
                foundUser.Name = user.Name;
                foundUser.Birthday = user.Birthday;
                foundUser.Email = user.Email;
                foundUser.Gender = user.Gender;
                foundUser.Role = user.Role;
                if (foundUser.PasswordHash != user.PasswordHash)
                {
                    foundUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash, BCrypt.Net.BCrypt.GenerateSalt());
                }

                _context.SaveChanges();
            }
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

