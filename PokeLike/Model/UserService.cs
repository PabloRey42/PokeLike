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

    // Méthode pour enregistrer un utilisateur
    public bool RegisterUser(string username, string password)
    {
        // Vérifiez si l'utilisateur existe déjà
        if (_context.Logins.Any(u => u.Username == username))
        {
            return false; // L'utilisateur existe déjà
        }

        // Hash du mot de passe
        var hashedPassword = HashPassword(password);

        // Créez un nouvel utilisateur
        var newUser = new Login
        {
            Username = username,
            PasswordHash = hashedPassword
        };

        _context.Logins.Add(newUser);
        _context.SaveChanges();
        return true;
    }

    // Méthode pour valider les identifiants
    public bool ValidateCredentials(string username, string password)
    {
        var user = _context.Logins.SingleOrDefault(u => u.Username == username);
        if (user == null)
        {
            return false; // Utilisateur non trouvé
        }

        // Vérifiez le mot de passe
        return user.PasswordHash == HashPassword(password);
    }

    // Fonction pour hasher un mot de passe (SHA-256)
    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
