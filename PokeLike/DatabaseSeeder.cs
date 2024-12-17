using System.Linq;
using PokeLike.Model;

namespace PokeLike.Services
{
    public static class DatabaseSeeder
    {
        public static void SeedDatabase(ExerciceMonsterContext context)
        {
            // Vérifiez si la base contient déjà des données
            if (!context.Monsters.Any() && !context.Spells.Any())
            {
                // Créer des sorts
                var thunderShock = new Spell { Name = "Thunder Shock", Damage = 40, Description = "A jolt of electricity strikes the target." };
                var quickAttack = new Spell { Name = "Quick Attack", Damage = 40, Description = "An extremely fast attack." };
                var thunderbolt = new Spell { Name = "Thunderbolt", Damage = 90, Description = "A strong electric blast crashes down on the target." };
                var ironTail = new Spell { Name = "Iron Tail", Damage = 100, Description = "The target is slammed with a steel-hard tail." };

                var ember = new Spell { Name = "Ember", Damage = 40, Description = "A small flame attack." };
                var flamethrower = new Spell { Name = "Flamethrower", Damage = 90, Description = "The target is scorched with flames." };
                var scratch = new Spell { Name = "Scratch", Damage = 40, Description = "Scratches the target with sharp claws." };
                var dragonRage = new Spell { Name = "Dragon Rage", Damage = 60, Description = "Generates a shock wave of dragon power." };

                // Créer des monstres avec leurs sorts associés
                var pikachu = new Monster
                {
                    Name = "Pikachu",
                    Health = 100,
                    Spells = { thunderShock, quickAttack, thunderbolt, ironTail }
                };

                var charmander = new Monster
                {
                    Name = "Charmander",
                    Health = 95,
                    Spells = { ember, flamethrower, scratch, dragonRage }
                };

                var squirtle = new Monster
                {
                    Name = "Squirtle",
                    Health = 110,
                    Spells = {
                        new Spell { Name = "Water Gun", Damage = 40, Description = "Shoots a stream of water at the target." },
                        new Spell { Name = "Bubble", Damage = 40, Description = "Generates a bubble attack to hit the target." },
                        new Spell { Name = "Hydro Pump", Damage = 110, Description = "Shoots a strong jet of water at the target." },
                        new Spell { Name = "Bite", Damage = 60, Description = "Bites the target with sharp teeth." }
                    }
                };

                var bulbasaur = new Monster
                {
                    Name = "Bulbasaur",
                    Health = 105,
                    Spells = {
                        new Spell { Name = "Vine Whip", Damage = 45, Description = "Strikes the target with slender vines." },
                        new Spell { Name = "Razor Leaf", Damage = 55, Description = "Cuts the target with sharp-edged leaves." },
                        new Spell { Name = "Solar Beam", Damage = 120, Description = "A powerful beam of sunlight." },
                        new Spell { Name = "Leech Seed", Damage = 0, Description = "Plants a seed on the target to drain health." }
                    }
                };

                var eevee = new Monster
                {
                    Name = "Eevee",
                    Health = 95,
                    Spells = { quickAttack, scratch, thunderbolt, flamethrower }
                };

                // Ajouter les données à la base
                context.Monsters.AddRange(pikachu, charmander, squirtle, bulbasaur, eevee);
                context.SaveChanges();
            }
        }
    }
}
