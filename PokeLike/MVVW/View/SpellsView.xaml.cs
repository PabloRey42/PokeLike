using System.Linq;
using System.Windows.Controls;
using PokeLike.Model;

namespace PokeLike.MVVW.View
{
    public partial class SpellsView : Page
    {
        private readonly ExerciceMonsterContext _context;

        public SpellsView(ExerciceMonsterContext context)
        {
            InitializeComponent();
            _context = context;

            // Charger les sorts
            LoadSpells();
        }

        private void LoadSpells()
        {
            var spells = _context.Spells.ToList();
            SpellsListView.ItemsSource = spells;
        }

        private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Retourner à la page précédente
            this.NavigationService.GoBack();
        }
    }
}
