using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using PokeLike.Model;

namespace PokeLike.MVVW.View
{
    public partial class LoginView : Page
    {
        private readonly UserService _userService;

        public LoginView()
        {
            InitializeComponent();

            // Initialisation du contexte et du service
            var optionsBuilder = new DbContextOptionsBuilder<ExerciceMonsterContext>();
            optionsBuilder.UseSqlServer("Server=Hum;Database=ExerciceMonster;Trusted_Connection=True;TrustServerCertificate=True;");
            var dbContext = new ExerciceMonsterContext(optionsBuilder.Options);
            _userService = new UserService(dbContext);
        }

        private void OnImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show($"Erreur de chargement de l'image : {e.ErrorException.Message}");
        }

        // Gestion du placeholder pour le TextBox (Nom d'utilisateur)
        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UsernamePlaceholder.Visibility = string.IsNullOrEmpty(UsernameTextBox.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        // Gestion du placeholder pour le PasswordBox (Mot de passe)
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(PasswordBox.Password)
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        // Gestion de la connexion
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return;
            }

            // Valider les identifiants
            if (_userService.ValidateCredentials(username, password))
            {
                MessageBox.Show("Connexion réussie !");
                // Naviguer ou effectuer une autre action si nécessaire
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect !");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.Navigate(new RegisterView());
        }
    }
}
