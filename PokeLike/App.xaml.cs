using System.Collections.Generic;
using System.Windows;

namespace PokeLike
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                // Liste des chemins des musiques
                var playlist = new List<string>
                {
                    "Assets/Music1.mp3",
                    "Assets/Music2.mp3",
                    "Assets/Music3.mp3"
                };

                // Charger la playlist
                var musicManager = BackgroundMusicManager.Instance;
                musicManager.LoadPlaylist(playlist);

                // Démarrer la musique
                musicManager.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur au démarrage de l'application : {ex.Message}");
            }
        }
    }
}
