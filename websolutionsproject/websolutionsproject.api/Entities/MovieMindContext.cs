using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace websolutionsproject.api.Entities
{
    public class MovieMindContext : IdentityDbContext<
        User,
        Role,
        Guid,
        IdentityUserClaim<Guid>,
        UserRole,
        IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>>        
    {
        public MovieMindContext(DbContextOptions<MovieMindContext> options)
            : base(options)
        { }

        public DbSet<Movie> Movies { get; set; }

        //public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Director> Directors { get; set; }

        //many-to-many
        public DbSet<ActorMovie> ActorMovies { get; set; }
        public DbSet<UserFollower> UserFollowers { get; set; }

        //public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        //Cascdading issue fixed with fluent API

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (modelBuilder == null ) { throw new ArgumentNullException(nameof(modelBuilder)); }

            modelBuilder.Entity<Role>(x =>
            {
                x.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
            });

            modelBuilder.Entity<User>(x =>
            {
                x.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });

            modelBuilder.Entity<RefreshToken>(x =>
            {
                x.HasOne(x => x.User)
                .WithMany(x => x.RefreshTokens)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
            });

            //Movie
            modelBuilder.Entity<Movie>(x =>
            {
                x.HasOne(m => m.Genre)
                .WithMany(e => e.Movies)
                .HasForeignKey(m => m.GenreId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                x.HasOne(e => e.Director)
                .WithMany(e => e.Movies)
                .HasForeignKey(e => e.DirectorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            //Review

            modelBuilder.Entity<Review>(x =>
            {
                x.HasOne(e => e.Movie)
                .WithMany(e => e.Reviews)
                .HasForeignKey(e => e.MovieId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                x.HasOne(e => e.User)
                .WithMany(e => e.Reviews)
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            //UserFollower
            modelBuilder.Entity<UserFollower>(x =>
            {
                x.HasKey(e => new { e.FollowingId, e.FollowerId });

                x.HasOne(e => e.Following)
                .WithMany(e => e.UserFollowers)
                .HasForeignKey(e => e.FollowingId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                x.HasOne(e => e.Follower)
                .WithMany(e => e.UserFollowing)
                .HasForeignKey(e => e.FollowerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            //ActorMovie
            modelBuilder.Entity<ActorMovie>(x =>
            {
                //x.HasKey(e => new { e.ActorId, e.MovieId });

                x.HasOne(e => e.Actor)
                .WithMany(e => e.ActorMovies)
                .HasForeignKey(e => e.ActorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                x.HasOne(e => e.Movie)
                .WithMany(e => e.ActorMovies)
                .HasForeignKey(e => e.MovieId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            //Favorites (user-movie)
            modelBuilder.Entity<Favorite>(x =>
            {
                x.HasKey(e => new { e.MovieId, e.UserId });

                x.HasOne(e => e.User)
                .WithMany(e => e.Favorites)
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                x.HasOne(e => e.Movie)
                .WithMany(e => e.Favorites)
                .HasForeignKey(e => e.MovieId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            // Indexes

            modelBuilder.Entity<Actor>(x =>
            {
                x.HasIndex(x => new { x.FirstName, x.LastName, x.Birth })
                .IsUnique(true)
                .HasName("UQ_Actor_FirstName_LastName_Birth");
            });

            modelBuilder.Entity<ActorMovie>(x =>
            {
                x.HasIndex(x => new { x.ActorId, x.MovieId })
                .IsUnique(true)
                .HasName("UQ_ActorMovie_ActorId_MovieId");
            });

            modelBuilder.Entity<Director>(x =>
            {
                x.HasIndex(x => new { x.FirstName, x.LastName, x.Birth })
                .IsUnique(true)
                .HasName("UQ_Director_FirstName_LastName_Birth");
            });

            modelBuilder.Entity<Favorite>(x =>
            {
                x.HasIndex(x => new { x.UserId, x.MovieId })
                .IsUnique(true)
                .HasName("UQ_Favorite_UserId_MovieId");
            });

            modelBuilder.Entity<Genre>(x =>
            {
                x.HasIndex(x => new { x.Name })
                .IsUnique(true)
                .HasName("UQ_Genre_Name");
            });

            modelBuilder.Entity<Movie>(x =>
            {
                x.HasIndex(x => new { x.Name, x.Year })
                .IsUnique(true)
                .HasName("UQ_Movie_Name_Year");
            });

            modelBuilder.Entity<Review>(x =>
            {
                x.HasIndex(x => new { x.Rating, x.Date, x.UserId, x.MovieId })
                .IsUnique(true)
                .HasName("UQ_Review_Rating_Date_UserId_MovieId");
            });

            modelBuilder.Entity<UserFollower>(x =>
            {
                x.HasIndex(x => new { x.FollowerId, x.FollowingId })
                .IsUnique(true)
                .HasName("UQ_UserFollower_FollowerId_FollowingId");
            });
        }
    }
}
