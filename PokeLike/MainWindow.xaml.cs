using System.Windows;
using PokeLike.MVVW.View;

namespace PokeLike
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Charger MainView dans la Frame principale
            MainFrame.Navigate(new MainView(MainFrame));
        }
    }
}
