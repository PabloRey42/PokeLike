using System.Linq;
using System.Security.Cryptography;
using System.Text;
using PokeLike.Model;

public class UserService
{
    private readonly ExerciceMonsterContext _context;

    public UserService(ExerciceMonsterContext context)
    {
        _context = context;
    }

    public bool RegisterUser(string username, string password)
    {
        if (_context.Logins.Any(u => u.Username == username))
        {
            return false; 
        }

        var hashedPassword = HashPassword(password);

        var newUser = new Login
        {
            Username = username,
            PasswordHash = hashedPassword
        };

        _context.Logins.Add(newUser);
        _context.SaveChanges();
        return true;
    }

    public bool ValidateCredentials(string username, string password)
    {
        var user = _context.Logins.SingleOrDefault(u => u.Username == username);
        if (user == null)
        {
            return false;
        }

        return user.PasswordHash == HashPassword(password);
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
