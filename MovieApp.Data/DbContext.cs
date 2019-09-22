using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        {
        }
        //Fluent Api configure relationships 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavoriteUserMovies>()
                .HasOne(a => a.User)
                .WithMany(a => a.FavoriteUserMovies)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<FavoriteUserMovies>()
                .HasOne(a => a.Movie)
                .WithMany(a => a.FavoriteUserMovies)
                .HasForeignKey(a => a.MovieId);

            //modelBuilder.Entity<File>()
            //    .HasOne(a => a.Movie)
            //    .WithOne(a=> a.Image)
            //    .HasForeignKey<Movie>(b=>b.ImageId);

            modelBuilder.Entity<Movie>()
                .HasOne(a => a.Image)
                .WithOne()
                .HasForeignKey<Movie>(b => b.ImageId);

        }

        DbSet<User> User { get; set; }
        DbSet<Movie> Movie { get; set; }
        DbSet<Image> File { get; set; }
        DbSet<FavoriteUserMovies> FavoriteUserMovies { get; set; }
    }
}
