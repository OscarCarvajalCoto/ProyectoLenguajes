using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProyectoLenguajes.Models;

namespace ProyectoLenguajes.Data;

public partial class BaseLenguajesContext : DbContext
{
    public BaseLenguajesContext()
    {
    }

    public BaseLenguajesContext(DbContextOptions<BaseLenguajesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Episode> Episodes { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<MoviesAndSeries> MoviesAndSeries { get; set; }

    public virtual DbSet<MoviesAndSeriesActor> MoviesAndSeriesActors { get; set; }

    public virtual DbSet<MoviesAndSeriesGenre> MoviesAndSeriesGenres { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=163.178.173.130;User id=basesdedatos;password=rpbases.2022;Database=proyectoLenguajesDSW;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.actor_id);

            entity.Property(e => e.actor_id).ValueGeneratedNever();
            entity.Property(e => e.actor_first_name)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.actor_last_name)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.comment_id);

            entity.Property(e => e.comment_id).ValueGeneratedNever();
            entity.Property(e => e.comment1)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("comment");
        });

        modelBuilder.Entity<Episode>(entity =>
        {
            entity.HasKey(e => e.episode_id);

            entity.Property(e => e.episode_id).ValueGeneratedNever();
            entity.Property(e => e.title)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.genre_id);

            entity.Property(e => e.genre_id).ValueGeneratedNever();
            entity.Property(e => e.genre_name)
                .HasMaxLength(30)
                .IsFixedLength();
        });

        modelBuilder.Entity<MoviesAndSeries>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK_MoviesandSeries");

            entity.Property(e => e.id).ValueGeneratedNever();
            entity.Property(e => e.classificacion)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.director)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.movie_cover)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.synopsis)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.title)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.year_of_release).HasColumnType("date");
        });

        modelBuilder.Entity<MoviesAndSeriesActor>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.actor).WithMany()
                .HasForeignKey(d => d.actor_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MoviesAnd__actor__32E0915F");

            entity.HasOne(d => d.movies_series).WithMany()
                .HasForeignKey(d => d.movies_series_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MoviesAnd__movie__33D4B598");
        });

        modelBuilder.Entity<MoviesAndSeriesGenre>(entity =>
        {
            entity.HasKey(e => e.movies_series_id);

            entity.Property(e => e.movies_series_id).ValueGeneratedNever();

            entity.HasOne(d => d.genre).WithMany(p => p.MoviesAndSeriesGenres)
                .HasForeignKey(d => d.genre_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MoviesAnd__genre__34C8D9D1");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.rating_id);

            entity.Property(e => e.rating_id).ValueGeneratedNever();
            entity.Property(e => e.rating1).HasColumnName("rating");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.user_id);

            entity.Property(e => e.user_id).ValueGeneratedNever();
            entity.Property(e => e.email)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.tipo)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.username)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
