using System.Windows;

namespace PokeLike
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Lancer la MainWindow sans se connecter immédiatement à la base de données
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
