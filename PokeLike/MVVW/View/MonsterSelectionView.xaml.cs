using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using PokeLike.Model;

namespace PokeLike.MVVW.View
{
    public partial class MonsterSelectionView : Page
    {
        private readonly ExerciceMonsterContext _context;
        private readonly Frame _mainFrame;
        private Monster _selectedMonster;

        public MonsterSelectionView(ExerciceMonsterContext context, Frame mainFrame)
        {
            InitializeComponent();
            _context = context;
            _mainFrame = mainFrame;

            LoadMonsters();
        }

        private void LoadMonsters()
        {
            var monsters = _context.Monsters.Include(m => m.Spells).ToList();
            MonsterListView.ItemsSource = monsters;
        }

        private void MonsterListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MonsterListView.SelectedItem is Monster selectedMonster)
            {
                _selectedMonster = selectedMonster; // Mettre à jour le monstre sélectionné

                // Mettre à jour les détails affichés
                MonsterNameText.Text = selectedMonster.Name;
                MonsterHPText.Text = selectedMonster.Health.ToString();
                MonsterSpellsList.ItemsSource = selectedMonster.Spells.ToList();
            }
        }

        private void ChooseMonsterButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedMonster != null)
            {
                MessageBox.Show($"Vous avez choisi {_selectedMonster.Name} !", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);

                // Naviguer vers BattlePage en passant le monstre sélectionné et la Frame
                _mainFrame.Navigate(new BattlePage(_selectedMonster, _mainFrame));
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un Pokémon d'abord.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
