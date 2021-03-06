﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class FilmContext : DbContext
    {

        public FilmContext() : base("DefaultConnection") { }
        public DbSet<Film> Films { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }

        public DbSet<MCinema> Cinemas { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasMany(g => g.Films)
                .WithMany(f => f.Genres)
                .Map(t => t.MapLeftKey("GenreId")
                .MapRightKey("FilmId")
                .ToTable("GenreFilm"));

            modelBuilder.Entity<Actor>().HasMany(a => a.Films)
                .WithMany(f => f.Actors)
                .Map(t => t.MapLeftKey("ActorId")
                .MapRightKey("FilmId")
                .ToTable("ActorFilm"));
        }
    }
    
}