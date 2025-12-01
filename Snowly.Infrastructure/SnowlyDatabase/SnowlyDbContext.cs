using Microsoft.EntityFrameworkCore;
using Snowly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Infrastructure.SnowlyDatabase
{
    public class SnowlyDbContext : DbContext
    {
        public SnowlyDbContext(DbContextOptions<SnowlyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<FriendShip> Friendships { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserConfirmCode> UserConfirmCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<Message>()
                .HasOne(m => m.SenderUser)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.ReceiverUser)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FriendShip>()
                .HasOne(fs => fs.Requester)
                .WithMany(u => u.FriendshipsSent)
                .HasForeignKey(fs => fs.RequesterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FriendShip>()
                .HasOne(fs => fs.Addressee)
                .WithMany(u => u.FriendshipsReceived)
                .HasForeignKey(fs => fs.AddresseeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserGroup>()
                .HasKey(ug => new { ug.UserId, ug.GroupId });
        }
    }
}
