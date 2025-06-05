using DataAccessLayer.Models;


namespace DataAccessLayer.Repositories
{
    public class UserRepository
    {
        private readonly MatrixIncDbContext _context;

        public UserRepository(MatrixIncDbContext context)
        {
            _context = context;
        }

        public User? GetByCredentials(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;
            }

            return null;
        }
    }
}
