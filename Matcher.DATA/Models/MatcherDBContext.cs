using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Matcher.DATA.Models
{
    public partial class MatcherDBContext : DbContext
    {
        public MatcherDBContext()
        {
        }

        public MatcherDBContext(DbContextOptions<MatcherDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; } = null!;
        public virtual DbSet<ChatRoom> ChatRooms { get; set; } = null!;
        public virtual DbSet<MatchOption> MatchOptions { get; set; } = null!;
        public virtual DbSet<Option> Options { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserChatRoom> UserChatRooms { get; set; } = null!;
        public virtual DbSet<UserLike> UserLikes { get; set; } = null!;
        public virtual DbSet<UserMatchOption> UserMatchOptions { get; set; } = null!;
        public virtual DbSet<UserOption> UserOptions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=DB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ChatRoomId).HasColumnName("ChatRoomID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MediaUrl).HasColumnName("MediaURL");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.ChatRoom)
                    .WithMany(p => p.ChatMessages)
                    .HasForeignKey(d => d.ChatRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChatMessages_ChatRooms");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ChatMessages)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChatMessages_Users");
            });

            modelBuilder.Entity<ChatRoom>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MatchOption>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MatchOptionId).HasColumnName("MatchOptionID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.MatchOption)
                    .WithMany(p => p.Options)
                    .HasForeignKey(d => d.MatchOptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Options_MatchOptions");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(20);

                entity.Property(e => e.PhoneNumber).HasMaxLength(15);

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(20);
            });

            modelBuilder.Entity<UserChatRoom>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ChatRoomId).HasColumnName("ChatRoomID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.ChatRoom)
                    .WithMany(p => p.UserChatRooms)
                    .HasForeignKey(d => d.ChatRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserChatRooms_ChatRooms");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserChatRooms)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserChatRooms_Users");
            });

            modelBuilder.Entity<UserLike>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LikedUserId).HasColumnName("LikedUserID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.LikedUser)
                    .WithMany(p => p.UserLikeLikedUsers)
                    .HasForeignKey(d => d.LikedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserLikes_Users1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLikeUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserLikes_Users");
            });

            modelBuilder.Entity<UserMatchOption>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MatchOptionId).HasColumnName("MatchOptionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.MatchOption)
                    .WithMany(p => p.UserMatchOptions)
                    .HasForeignKey(d => d.MatchOptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserMatchOptions_MatchOptions");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserMatchOptions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserMatchOptions_Users");
            });

            modelBuilder.Entity<UserOption>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.OptionId).HasColumnName("OptionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.UserOptions)
                    .HasForeignKey(d => d.OptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserOptions_Options");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserOptions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserOptions_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
