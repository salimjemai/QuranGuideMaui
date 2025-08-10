using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using QuranGuide.Maui.Core.Models;
using QuranGuide.Maui.Core.DataTransferObjects;

namespace QuranGuide.Maui.Infrastructure
{
    public class QuranDbContext : DbContext
    {
        public DbSet<Surah> Surahs { get; set; }
        public DbSet<Ayah> Ayahs { get; set; }
        public DbSet<Edition> Editions { get; set; }
        public DbSet<Hadith> Hadiths { get; set; }
        public DbSet<UserPreferences> UserPreferences { get; set; }
        public DbSet<DownloadedContent> DownloadedContent { get; set; }

        public QuranDbContext(DbContextOptions<QuranDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships and constraints
            modelBuilder.Entity<Surah>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Number).IsRequired();
                entity.HasIndex(e => e.Number).IsUnique();
            });

            modelBuilder.Entity<Ayah>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Text).IsRequired();
                entity.HasIndex(e => new { e.SurahId, e.NumberInSurah, e.EditionId });
            });

            // Treat UserPreferences as a keyless read-only type (not mapped to a table by migrations)
            modelBuilder.Entity<UserPreferences>().HasNoKey();

            // Configure other entities...
        }
    }

}
