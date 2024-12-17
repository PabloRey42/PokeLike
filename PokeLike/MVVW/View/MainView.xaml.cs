using System.Windows;
using System.Windows.Controls;

namespace PokeLike.MVVW.View
{
    public partial class MainView : Page
    {
        private readonly Frame _mainFrame;

        public MainView(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new LoginView(_mainFrame));
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new OptionsPage(_mainFrame));
        }

        private void OnImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show($"Erreur de chargement de l'image : {e.ErrorException.Message}");
        }
    }
}
