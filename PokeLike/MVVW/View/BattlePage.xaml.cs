using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore;
using PokeLike.Model;

namespace PokeLike.MVVW.View
{
    public partial class BattlePage : Page
    {
        private readonly Monster _playerMonster;
        private Monster _enemyMonster;
        private readonly Random _random = new Random();
        private readonly Frame _mainFrame;

        private double _enemyHealthMultiplier = 1.2; // Multiplieur pour le boost des HP

        public BattlePage(Monster playerMonster, Frame mainFrame)
        {
            InitializeComponent();
            _playerMonster = playerMonster;
            _mainFrame = mainFrame;

            InitializeBattle();
        }

        private void InitializeBattle()
        {
            // Initialisation du Pokémon joueur
            SetupMonsterUI(PlayerMonsterName, PlayerHPBar, PlayerHPText, _playerMonster);

            // Générer le premier ennemi
            GenerateNewEnemy();

            // Configuration des attaques
            ConfigureAttackButtons();
        }

        private void ConfigureAttackButtons()
        {
            var spells = _playerMonster.Spells.ToList();

            Button[] attackButtons = { AttackButton1, AttackButton2, AttackButton3, AttackButton4 };

            for (int i = 0; i < attackButtons.Length; i++)
            {
                if (i < spells.Count)
                {
                    attackButtons[i].Content = $"{spells[i].Name} ({spells[i].Damage} dégâts)";
                    attackButtons[i].IsEnabled = true;
                }
                else
                {
                    attackButtons[i].Content = "Aucune attaque";
                    attackButtons[i].IsEnabled = false;
                }
            }
        }

        private void GenerateNewEnemy()
        {
            int boostedHP = (int)(100 * _enemyHealthMultiplier);
            _enemyMonster = new Monster
            {
                Name = "Pokémon Sauvage Boosté",
                Health = boostedHP,
                Spells = _playerMonster.Spells.ToList()
            };

            SetupMonsterUI(EnemyMonsterName, EnemyHPBar, EnemyHPText, _enemyMonster);
            _enemyHealthMultiplier += 0.2; // Boost des HP pour les prochains ennemis
        }

        private void SetupMonsterUI(TextBlock nameText, ProgressBar hpBar, TextBlock hpText, Monster monster)
        {
            nameText.Text = monster.Name;
            hpBar.Maximum = monster.Health;
            hpBar.Value = monster.Health;
            hpText.Text = $"HP: {monster.Health}/{monster.Health}";

            UpdateProgressBarColor(hpBar, monster.Health);
        }

        private void AttackButton_Click(object sender, RoutedEventArgs e)
        {
            if (_enemyMonster.Health <= 0 || _playerMonster.Health <= 0) return;

            var button = sender as Button;
            int spellIndex = int.Parse(button.Name.Last().ToString()) - 1;
            var spell = _playerMonster.Spells.ToList()[spellIndex];

            ApplyDamage(_enemyMonster, spell.Damage, $"{_playerMonster.Name} utilise {spell.Name}");
            UpdateEnemyHP();

            if (_enemyMonster.Health > 0)
            {
                EnemyAttack();
            }
            else
            {
                GenerateNewEnemy();
            }

            CheckGameOver();
        }

        private void EnemyAttack()
        {
            var enemySpell = _enemyMonster.Spells.ToList()[_random.Next(_enemyMonster.Spells.Count)];
            ApplyDamage(_playerMonster, enemySpell.Damage, $"{_enemyMonster.Name} utilise {enemySpell.Name}");
            UpdatePlayerHP();
        }

        private void ApplyDamage(Monster target, int damage, string message)
        {
            target.Health -= damage;
            if (target.Health < 0) target.Health = 0;

            MessageBox.Show($"{message} et inflige {damage} dégâts !");
        }

        private void UpdateEnemyHP()
        {
            UpdateHP(EnemyHPBar, EnemyHPText, _enemyMonster);
        }

        private void UpdatePlayerHP()
        {
            UpdateHP(PlayerHPBar, PlayerHPText, _playerMonster);
        }

        private void UpdateHP(ProgressBar hpBar, TextBlock hpText, Monster monster)
        {
            hpBar.Value = monster.Health;
            hpText.Text = $"HP: {monster.Health}/{hpBar.Maximum}";
            UpdateProgressBarColor(hpBar, monster.Health);
        }

        private void UpdateProgressBarColor(ProgressBar progressBar, double currentHP)
        {
            if (progressBar == null) return;

            double percentage = (currentHP / progressBar.Maximum) * 100;

            // Définir la couleur en fonction du pourcentage de HP
            if (percentage > 50)
                progressBar.Foreground = new SolidColorBrush(Colors.Green);
            else if (percentage > 25)
                progressBar.Foreground = new SolidColorBrush(Colors.Yellow);
            else
                progressBar.Foreground = new SolidColorBrush(Colors.Red);
        }


        private void CheckGameOver()
        {
            if (_playerMonster.Health <= 0)
            {
                MessageBox.Show($"{_playerMonster.Name} a été vaincu !", "Défaite", MessageBoxButton.OK, MessageBoxImage.Information);
                foreach (var button in new[] { AttackButton1, AttackButton2, AttackButton3, AttackButton4 })
                {
                    button.IsEnabled = false;
                }
            }
        }

        private void RechoosePokemon_Click(object sender, RoutedEventArgs e)
        {
            var optionsBuilder = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<ExerciceMonsterContext>();
            optionsBuilder.UseSqlServer("Server=VotreServeur;Database=VotreBaseDeDonnées;Trusted_Connection=True;TrustServerCertificate=True;");

            var context = new ExerciceMonsterContext(optionsBuilder.Options);
            _mainFrame.Navigate(new MonsterSelectionView(context, _mainFrame));
        }
    }
}
