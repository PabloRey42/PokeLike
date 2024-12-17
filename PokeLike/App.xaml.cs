using System.Windows;
using Microsoft.EntityFrameworkCore;
using PokeLike.Model;
using PokeLike.Services;

namespace PokeLike
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Configuration de la base de données
            var optionsBuilder = new DbContextOptionsBuilder<ExerciceMonsterContext>();
            optionsBuilder.UseSqlServer("Server=VotreServeur;Database=VotreBaseDeDonnées;Trusted_Connection=True;TrustServerCertificate=True;");

            using (var context = new ExerciceMonsterContext(optionsBuilder.Options))
            {
                // Seed de la base de données
                DatabaseSeeder.SeedDatabase(context);
            }

            // Création et lancement unique de MainWindow
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
