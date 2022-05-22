﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication2.Models
{
    public partial class CricketContext : DbContext
    {
        public CricketContext()
        {
        }

        public CricketContext(DbContextOptions<CricketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Matches> Matches { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<Stadium> Stadium { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Cricket;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.Continent)
                    .HasColumnName("continent")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCode)
                    .HasColumnName("country_code")
                    .HasMaxLength(30);

                entity.Property(e => e.CountryName)
                    .HasColumnName("country_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Matches>(entity =>
            {
                entity.HasKey(e => e.MatchId);

                entity.Property(e => e.MatchId).HasColumnName("Match_id");

                entity.Property(e => e.DateTime)
                    .HasColumnName("Date_Time")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Result)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.StadiumName)
                    .HasColumnName("Stadium_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TeamA).HasColumnName("Team_A");

                entity.Property(e => e.TeamB).HasColumnName("Team_B");

                entity.Property(e => e.WasMatchPlayed)
                    .HasColumnName("was_match_played")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.TeamANavigation)
                    .WithMany(p => p.MatchesTeamANavigation)
                    .HasForeignKey(d => d.TeamA)
                    .HasConstraintName("FK__Matches__Team_A__3F466844");

                entity.HasOne(d => d.TeamBNavigation)
                    .WithMany(p => p.MatchesTeamBNavigation)
                    .HasForeignKey(d => d.TeamB)
                    .HasConstraintName("FK__Matches__Team_B__403A8C7D");
            });

            modelBuilder.Entity<Players>(entity =>
            {
                entity.HasKey(e => e.PlayerId);

                entity.Property(e => e.PlayerId).HasColumnName("player_id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.PlayerAge).HasColumnName("player_age");

                entity.Property(e => e.PlayerName)
                    .HasColumnName("player_name")
                    .HasMaxLength(30);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__Players__country__398D8EEE");
            });

            modelBuilder.Entity<Stadium>(entity =>
            {
                entity.Property(e => e.StadiumId)
                    .HasColumnName("stadium_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.NoOfMatchesAllowed).HasColumnName("No_of_matches_allowed");

                entity.Property(e => e.StadiumCount).HasColumnName("stadium_count");

                entity.Property(e => e.StadiumName)
                    .HasColumnName("stadium_name")
                    .HasMaxLength(30);

                entity.HasOne(d => d.StadiumNavigation)
                    .WithOne(p => p.Stadium)
                    .HasForeignKey<Stadium>(d => d.StadiumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stadium__stadium__3C69FB99");
            });
        }
    }
}
