using System;
using System.Collections.Generic;
using DemoArchitechture.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoArchitechture.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.CharacterId).HasName("characters_pkey");

            entity.ToTable("characters");

            entity.Property(e => e.CharacterId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("character_id");
            entity.Property(e => e.CharacterAttack).HasColumnName("character_attack");
            entity.Property(e => e.CharacterHealth).HasColumnName("character_health");
            entity.Property(e => e.CharacterName)
                .HasMaxLength(32)
                .HasColumnName("character_name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Characters)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.UserEmail, "users_user_email_key").IsUnique();

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("user_id");
            entity.Property(e => e.IsBanned)
                .HasDefaultValue(false)
                .HasColumnName("is_banned");
            entity.Property(e => e.UserCreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("user_created_at");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(255)
                .HasColumnName("user_email");
            entity.Property(e => e.UserFullname)
                .HasMaxLength(64)
                .HasColumnName("user_fullname");
            entity.Property(e => e.UserPasswordHash)
                .HasMaxLength(255)
                .HasColumnName("user_password_hash");
            entity.Property(e => e.UserUpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("user_updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}