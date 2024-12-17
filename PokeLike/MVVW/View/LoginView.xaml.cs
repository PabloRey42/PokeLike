using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using PokeLike.Model;

namespace PokeLike.MVVW.View
{
    public partial class LoginView : Page
    {
        private readonly Frame _mainFrame; // Référence à la Frame pour la navigation
        private readonly UserService _userService;

        public LoginView(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame;

            // Initialiser le service utilisateur avec la base de données
            var dbContext = new ExerciceMonsterContext();
            _userService = new UserService(dbContext);
        }

        // Gestion du bouton "Créer un compte"
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Naviguer vers RegisterView
            _mainFrame.Navigate(new RegisterView(_mainFrame));
        }

        // Gestion du bouton "Se connecter"
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text; // Récupérer le nom d'utilisateur
            string password = PasswordBox.Password; // Récupérer le mot de passe

            // Validation des champs
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return;
            }

            // Vérification des identifiants
            if (_userService.ValidateCredentials(username, password))
            {
                MessageBox.Show("Connexion réussie !");

                // Navigation vers MonsterSelectionView après connexion
                var optionsBuilder = new DbContextOptionsBuilder<ExerciceMonsterContext>();
                optionsBuilder.UseSqlServer("Server=VotreServeur;Database=ExerciceMonster;Trusted_Connection=True;TrustServerCertificate=True;");
                var context = new ExerciceMonsterContext(optionsBuilder.Options);

                _mainFrame.Navigate(new MonsterSelectionView(context, _mainFrame));
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect !");
            }
        }

        // Placeholder pour le TextBox (nom d'utilisateur)
        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UsernamePlaceholder.Visibility = string.IsNullOrEmpty(UsernameTextBox.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        // Placeholder pour le PasswordBox (mot de passe)
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(PasswordBox.Password)
                ? Visibility.Visible
                : Visibility.Hidden;
        }
    }
}
