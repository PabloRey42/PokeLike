﻿using Microsoft.EntityFrameworkCore;
using System.IO;

namespace PokeLike.Model
{
    public partial class ExerciceMonsterContext : DbContext
    {
        public ExerciceMonsterContext()
        {
        }

        public ExerciceMonsterContext(DbContextOptions<ExerciceMonsterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Monster> Monsters { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Spell> Spells { get; set; }

        // Méthode pour récupérer le serveur
        private string GetServerName()
        {
            string defaultServer = "Hum"; // Valeur par défaut si aucun fichier n'existe
            string configFilePath = "dbconfig.txt";

            if (File.Exists(configFilePath))
            {
                string serverName = File.ReadAllText(configFilePath).Trim();
                if (!string.IsNullOrWhiteSpace(serverName))
                {
                    return serverName;
                }
            }

            return defaultServer;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string serverName = GetServerName(); // Récupérer le nom du serveur
            optionsBuilder.UseSqlServer($"Server={serverName};Database=ExerciceMonster;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Login__3214EC270E619A1C");

                entity.ToTable("Login");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.PasswordHash).HasMaxLength(255);
                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<Monster>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Monster__3214EC278978C546");

                entity.ToTable("Monster");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasMany(d => d.Spells).WithMany(p => p.Monsters)
                    .UsingEntity<Dictionary<string, object>>(
                        "MonsterSpell",
                        r => r.HasOne<Spell>().WithMany()
                            .HasForeignKey("SpellId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__MonsterSp__Spell__44FF419A"),
                        l => l.HasOne<Monster>().WithMany()
                            .HasForeignKey("MonsterId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__MonsterSp__Monst__440B1D61"),
                        j =>
                        {
                            j.HasKey("MonsterId", "SpellId").HasName("PK__MonsterS__293EA4DFE4D65DE9");
                            j.ToTable("MonsterSpell");
                            j.IndexerProperty<int>("MonsterId").HasColumnName("MonsterID");
                            j.IndexerProperty<int>("SpellId").HasColumnName("SpellID");
                        });
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Player__3214EC27C7A92CE9");

                entity.ToTable("Player");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.LoginId).HasColumnName("LoginID");
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Login).WithMany(p => p.Players)
                    .HasForeignKey(d => d.LoginId)
                    .HasConstraintName("FK__Player__LoginID__398D8EEE");

                entity.HasMany(d => d.Monsters).WithMany(p => p.Players)
                    .UsingEntity<Dictionary<string, object>>(
                        "PlayerMonster",
                        r => r.HasOne<Monster>().WithMany()
                            .HasForeignKey("MonsterId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__PlayerMon__Monst__412EB0B6"),
                        l => l.HasOne<Player>().WithMany()
                            .HasForeignKey("PlayerId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__PlayerMon__Playe__403A8C7D"),
                        j =>
                        {
                            j.HasKey("PlayerId", "MonsterId").HasName("PK__PlayerMo__378F20A4B9CED656");
                            j.ToTable("PlayerMonster");
                            j.IndexerProperty<int>("PlayerId").HasColumnName("PlayerID");
                            j.IndexerProperty<int>("MonsterId").HasColumnName("MonsterID");
                        });
            });

            modelBuilder.Entity<Spell>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Spell__3214EC27A874ABE8");

                entity.ToTable("Spell");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
