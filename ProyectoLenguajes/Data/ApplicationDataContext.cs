using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProyectoLenguajes.Models;

namespace ProyectoLenguajes.Data;

public partial class ApplicationDataContext : DbContext
{
    public ApplicationDataContext()
    {
    }

    public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Episode> Episodes { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie_Serie> Movie_Series { get; set; }

    public virtual DbSet<Movie_Serie_Actor> Movie_Serie_Actors { get; set; }

    public virtual DbSet<Movie_Serie_Genre> Movie_Serie_Genres { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=163.178.173.130;Database=WAO_PROJECT_DB;TrustServerCertificate=True;User Id=basesdedatos;Password=rpbases.2022");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.actor_id).HasName("pk_actor");

            entity.ToTable("Actor");

            entity.Property(e => e.actor_first_name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.actor_last_name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.comment_id).HasName("pk_comment");

            entity.ToTable("Comment");

            entity.Property(e => e.app_user).HasMaxLength(256);
            entity.Property(e => e.comment1).HasColumnName("comment");

            entity.HasOne(d => d.ms).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ms_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comment__ms_id__160F4887");
        });

        modelBuilder.Entity<Episode>(entity =>
        {
            entity.HasKey(e => new { e.ms_id, e.season_id, e.episode_id }).HasName("pk_episode");

            entity.ToTable("Episode");

            entity.Property(e => e.tittle)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.genre_id).HasName("pk_genre");

            entity.ToTable("Genre");

            entity.Property(e => e.genre_name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Movie_Serie>(entity =>
        {
            entity.HasKey(e => e.ms_id).HasName("pk_movie_serie");

            entity.ToTable("Movie_Serie");

            entity.Property(e => e.classificationMS)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.date_added).HasColumnType("date");
            entity.Property(e => e.director)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ms_type)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.tittle)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Movie_Serie_Actor>(entity =>
        {
            entity.HasKey(e => new { e.ms_id, e.actor_id }).HasName("pk_movie_serie_actor");

            entity.ToTable("Movie_Serie_Actor");
        });

        modelBuilder.Entity<Movie_Serie_Genre>(entity =>
        {
            entity.HasKey(e => new { e.ms_id, e.genre_id }).HasName("pk_movie_serie_genre");

            entity.ToTable("Movie_Serie_Genre");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.rating_id).HasName("pk_rating");

            entity.ToTable("Rating");

            entity.Property(e => e.app_user).HasMaxLength(256);
            entity.Property(e => e.rating1).HasColumnName("rating");

            entity.HasOne(d => d.ms).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.ms_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rating__ms_id__18EBB532");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
