using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ITnews.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITnews.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<New> News { get; set; }
        public DbSet<Сomment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserNew> UserNews { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<NewTag> NewTags { get; set; }

        public object DefaultAuthenticationTypes { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserComment>()
             .HasKey(t => new { t.СommentId, t.ApplicationUserId });

            modelBuilder.Entity<UserComment>()
                .HasOne(pt => pt.Comment)
                .WithMany(p => p.UserComments)
                .HasForeignKey(pt => pt.СommentId);

            modelBuilder.Entity<UserComment>()
                .HasOne(pt => pt.ApplicationUser)
                .WithMany(t => t.UserComments)
                .HasForeignKey(pt => pt.ApplicationUserId);

            modelBuilder.Entity<UserNew>()
            .HasKey(t => new { t.NewId, t.ApplicationUserId });

            modelBuilder.Entity<UserNew>()
                .HasOne(pt => pt.New)
                .WithMany(p => p.UserNew)
                .HasForeignKey(pt => pt.NewId);

            modelBuilder.Entity<UserNew>()
                .HasOne(pt => pt.ApplicationUser)
                .WithMany(t => t.UserNew)
                .HasForeignKey(pt => pt.ApplicationUserId);

            modelBuilder.Entity<NewTag>()
            .HasKey(t => new { t.NewId, t.TagId });

            modelBuilder.Entity<NewTag>()
                .HasOne(pt => pt.New)
                .WithMany(p => p.NewTag)
                .HasForeignKey(pt => pt.NewId);

            modelBuilder.Entity<NewTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.NewTag)
                .HasForeignKey(pt => pt.TagId);
        }

    }

}
