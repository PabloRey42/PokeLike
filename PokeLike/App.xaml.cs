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

            var optionsBuilder = new DbContextOptionsBuilder<ExerciceMonsterContext>();
            optionsBuilder.UseSqlServer("Server=VotreServeur;Database=VotreBaseDeDonnées;Trusted_Connection=True;TrustServerCertificate=True;");
            using (var context = new ExerciceMonsterContext(optionsBuilder.Options))
            {
                DatabaseSeeder.SeedDatabase(context);
            }
        }
    }
}
