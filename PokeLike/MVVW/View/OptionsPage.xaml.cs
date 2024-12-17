using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PokeLike.MVVW.View
{
    public partial class OptionsPage : Page
    {
        private readonly Frame _mainFrame;
        private const string ConfigFilePath = "dbconfig.txt"; // Fichier de configuration local

        public OptionsPage(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame;

            // Charger la valeur actuelle (si disponible)
            if (File.Exists(ConfigFilePath))
            {
                ServerInputTextBox.Text = File.ReadAllText(ConfigFilePath);
                PlaceholderTextBlock.Visibility = string.IsNullOrWhiteSpace(ServerInputTextBox.Text)
                    ? Visibility.Visible
                    : Visibility.Hidden;
            }
        }

        private void ServerInputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = string.IsNullOrWhiteSpace(ServerInputTextBox.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string newServer = ServerInputTextBox.Text;

            if (string.IsNullOrWhiteSpace(newServer))
            {
                MessageBox.Show("Le nom du serveur ne peut pas être vide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Sauvegarder dans un fichier de config local
            File.WriteAllText(ConfigFilePath, newServer);

            // Afficher un message de confirmation
            ConfirmationMessage.Text = "Serveur mis à jour avec succès !";

            // Naviguer vers MainView
            _mainFrame.Navigate(new MainView(_mainFrame));
        }

        public static string GetServerName()
        {
            const string defaultServer = "Hum"; // Valeur par défaut
            return File.Exists(ConfigFilePath) ? File.ReadAllText(ConfigFilePath) : defaultServer;
        }
    }
}
