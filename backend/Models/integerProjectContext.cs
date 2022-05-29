using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace backendAg.Models
{
    public partial class integerProjectC : DbContext
    {
        public integerProjectC()
        {
        }

        public integerProjectC(DbContextOptions<integerProjectC> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrator> Administrators { get; set; } = null!;
        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<Crop> Crops { get; set; } = null!;
        public virtual DbSet<Farmer> Farmers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;Database=integerProject;Port=1111;Username=postgres;Password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.ToTable("administrator");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"administrator_specialId_seq\"'::regclass)");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Username).HasColumnName("username");

                entity.Property(e => e.Token).HasColumnName("token");
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("article");

                entity.Property(e => e.ArticleId).HasColumnName("articleId");

                entity.Property(e => e.Author).HasColumnName("author");

                entity.Property(e => e.AuthorId).HasColumnName("authorID");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.PublicationDate).HasColumnName("publicationDate");

                entity.Property(e => e.Tittle).HasColumnName("tittle");

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("article_authorID_fkey");
            });

            modelBuilder.Entity<Crop>(entity =>
            {
                entity.ToTable("crop");

                entity.Property(e => e.CropId)
                    .HasColumnName("cropID")
                    .HasDefaultValueSql("nextval('\"crop_cropId_seq\"'::regclass)");

                entity.Property(e => e.CropName).HasColumnName("cropName");

                entity.Property(e => e.CropType).HasColumnName("cropType");

                entity.Property(e => e.FarmerId).HasColumnName("farmerID");

                entity.Property(e => e.SeedTime).HasColumnName("seedTime");

                entity.HasOne(d => d.Farmer)
                    .WithMany(p => p.Crops)
                    .HasForeignKey(d => d.FarmerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("crop_farmerID_fkey");
            });

            modelBuilder.Entity<Farmer>(entity =>
            {
                entity.ToTable("farmer");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"farmer_idFarmer_seq\"'::regclass)");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.UserName).HasColumnName("userName");

                entity.Property(e => e.Token).HasColumnName("token");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
