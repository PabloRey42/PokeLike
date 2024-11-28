using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using PokeLike.Model;

namespace PokeLike.MVVW.View
{
    public partial class RegisterView : Page
    {
        private readonly UserService _userService;

        public RegisterView()
        {
            InitializeComponent();

            var optionsBuilder = new DbContextOptionsBuilder<ExerciceMonsterContext>();
            optionsBuilder.UseSqlServer("Server=Hum;Database=ExerciceMonster;Trusted_Connection=True;TrustServerCertificate=True;");
            var dbContext = new ExerciceMonsterContext(optionsBuilder.Options);
            _userService = new UserService(dbContext);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Les mots de passe ne correspondent pas.");
                return;
            }

            if (_userService.RegisterUser(username, password))
            {
                MessageBox.Show("Compte créé avec succès !");
                this.NavigationService?.Navigate(new LoginView());
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur déjà utilisé !");
            }
        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UsernamePlaceholder.Visibility = string.IsNullOrEmpty(UsernameTextBox.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(PasswordBox.Password)
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ConfirmPasswordPlaceholder.Visibility = string.IsNullOrEmpty(ConfirmPasswordBox.Password)
                ? Visibility.Visible
                : Visibility.Hidden;
        }
    }
}
