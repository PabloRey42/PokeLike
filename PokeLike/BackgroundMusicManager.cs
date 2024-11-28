using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;

namespace PokeLike
{
    public class BackgroundMusicManager
    {
        private static BackgroundMusicManager _instance;
        private readonly MediaPlayer _mediaPlayer;
        private readonly List<string> _musicPaths;
        private int _currentTrackIndex;

        private BackgroundMusicManager()
        {
            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.MediaEnded += OnTrackEnded;
            _musicPaths = new List<string>();
            _currentTrackIndex = 0;
        }

        public static BackgroundMusicManager Instance => _instance ??= new BackgroundMusicManager();

        public void LoadPlaylist(IEnumerable<string> musicPaths)
        {
            try
            {
                _musicPaths.Clear();
                _musicPaths.AddRange(musicPaths);
                _currentTrackIndex = 0;

                MessageBox.Show($"Playlist chargée avec {_musicPaths.Count} musiques.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement de la playlist : {ex.Message}");
            }
        }

        public void Play()
        {
            if (_musicPaths.Count == 0)
            {
                MessageBox.Show("Aucune musique n'est disponible dans la playlist !");
                return;
            }

            try
            {
                string trackPath = _musicPaths[_currentTrackIndex];
                _mediaPlayer.Open(new Uri(trackPath, UriKind.RelativeOrAbsolute));
                _mediaPlayer.Play();
                MessageBox.Show($"Lecture de la musique : {trackPath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la lecture de la musique : {ex.Message}");
            }
        }

        public void NextTrack()
        {
            if (_musicPaths.Count == 0)
            {
                MessageBox.Show("Playlist vide. Impossible de passer à la piste suivante.");
                return;
            }

            _currentTrackIndex = (_currentTrackIndex + 1) % _musicPaths.Count;
            MessageBox.Show($"Passage à la musique suivante : {_currentTrackIndex + 1}/{_musicPaths.Count}");
            Play();
        }

        public void PreviousTrack()
        {
            if (_musicPaths.Count == 0)
            {
                MessageBox.Show("Playlist vide. Impossible de revenir à la piste précédente.");
                return;
            }

            _currentTrackIndex = (_currentTrackIndex - 1 + _musicPaths.Count) % _musicPaths.Count;
            MessageBox.Show($"Retour à la musique précédente : {_currentTrackIndex + 1}/{_musicPaths.Count}");
            Play();
        }

        public void Stop()
        {
            _mediaPlayer.Stop();
            MessageBox.Show("Musique arrêtée.");
        }

        public void Pause()
        {
            _mediaPlayer.Pause();
            MessageBox.Show("Musique mise en pause.");
        }

        public void SetVolume(double volume)
        {
            _mediaPlayer.Volume = volume;
            MessageBox.Show($"Volume réglé sur : {volume * 100}%");
        }

        private void OnTrackEnded(object sender, EventArgs e)
        {
            MessageBox.Show("Musique terminée. Passage à la suivante.");
            NextTrack();
        }
    }
}
