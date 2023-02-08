using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UserAPI.Models
{
    public partial class User_DBContext : DbContext
    {
        public User_DBContext()
        {
        }

        public User_DBContext(DbContextOptions<User_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserDetails> UserDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDetails>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_user_id")
                    .IsClustered(false);

                entity.ToTable("user_details_aoife");

                entity.HasIndex(e => e.EmailAddress, "uq_aoife_email_address")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email_address");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .HasColumnName("last_name");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.StatusCode).HasColumnName("status_code");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
