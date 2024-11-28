using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PokeLike.MVVW.View
{
    public partial class MainView : Page
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void OnImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show($"Erreur de chargement de l'image : {e.ErrorException.Message}");
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoginView());
        }

        private void PreviousTrack_Click(object sender, RoutedEventArgs e)
        {
            BackgroundMusicManager.Instance.PreviousTrack();
        }

        private void NextTrack_Click(object sender, RoutedEventArgs e)
        {
            BackgroundMusicManager.Instance.NextTrack();
        }

        private void PauseMusic_Click(object sender, RoutedEventArgs e)
        {
            BackgroundMusicManager.Instance.Pause();
        }

        private void PlayMusic_Click(object sender, RoutedEventArgs e)
        {
            BackgroundMusicManager.Instance.Play();
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BackgroundMusicManager.Instance.SetVolume(e.NewValue);
        }



    }
}
