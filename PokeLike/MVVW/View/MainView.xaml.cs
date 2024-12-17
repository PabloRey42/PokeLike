using System.Windows;
using System.Windows.Controls;

namespace PokeLike.MVVW.View
{
    public partial class MainView : Page
    {
        private readonly Frame _mainFrame;

        // Constructeur par défaut requis par WPF
        public MainView()
        {
            InitializeComponent();
        }

        // Constructeur avec Frame pour la navigation
        public MainView(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame;
        }

        // Bouton pour démarrer le jeu et naviguer vers LoginView
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Naviguer vers LoginView en passant la même Frame
            _mainFrame?.Navigate(new LoginView(_mainFrame));
        }

        // Gestion des erreurs d'image
        private void OnImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show($"Erreur de chargement de l'image : {e.ErrorException.Message}");
        }
    }
}
