using System;
using System.Collections.Generic;
using DemoArchitechture.Models;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<CharacterSession> CharacterSessions { get; set; }

    public virtual DbSet<Consumable> Consumables { get; set; }

    public virtual DbSet<Enemy> Enemies { get; set; }

    public virtual DbSet<EnemyItem> EnemyItems { get; set; }

    public virtual DbSet<Gear> Gears { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Level> Levels { get; set; }

    public virtual DbSet<PlayerItem> PlayerItems { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WorldState> WorldStates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=DemoDB;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.CharacterId).HasName("characters_pkey");

            entity.ToTable("characters");

            entity.HasIndex(e => e.Username, "characters_username_key").IsUnique();

            entity.Property(e => e.CharacterId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("character_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.LastLogin).HasColumnName("last_login");
            entity.Property(e => e.TotalPlaytime)
                .HasDefaultValue(0)
                .HasColumnName("total_playtime");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.User).WithMany(p => p.Characters)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("characters_user_id_fkey");
        });

        modelBuilder.Entity<CharacterSession>(entity =>
        {
            entity.HasKey(e => new { e.CharacterId, e.SessionId }).HasName("character_session_pkey");

            entity.ToTable("character_session");

            entity.Property(e => e.CharacterId).HasColumnName("character_id");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.AttackSpeed).HasColumnName("attack_speed");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrentExp)
                .HasDefaultValue(0)
                .HasColumnName("current_exp");
            entity.Property(e => e.CurrentHp).HasColumnName("current_hp");
            entity.Property(e => e.CurrentLevel)
                .HasDefaultValue(1)
                .HasColumnName("current_level");
            entity.Property(e => e.CurrentMana).HasColumnName("current_mana");
            entity.Property(e => e.MaxHp).HasColumnName("max_hp");
            entity.Property(e => e.MaxMana).HasColumnName("max_mana");
            entity.Property(e => e.MaxStamina).HasColumnName("max_stamina");
            entity.Property(e => e.MovementSpeed).HasColumnName("movement_speed");
            entity.Property(e => e.PlayerRole).HasColumnName("player_role");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorldLocation)
                .HasColumnType("jsonb")
                .HasColumnName("world_location");

            entity.HasOne(d => d.Character).WithMany(p => p.CharacterSessions)
                .HasForeignKey(d => d.CharacterId)
                .HasConstraintName("character_session_character_id_fkey");

            entity.HasOne(d => d.Session).WithMany(p => p.CharacterSessions)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("character_session_session_id_fkey");

            entity.HasMany(d => d.Skills).WithMany(p => p.CharacterSessions)
                .UsingEntity<Dictionary<string, object>>(
                    "PlayerSkill",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillId")
                        .HasConstraintName("player_skill_skill_id_fkey"),
                    l => l.HasOne<CharacterSession>().WithMany()
                        .HasForeignKey("CharacterId", "SessionId")
                        .HasConstraintName("player_skill_character_id_session_id_fkey"),
                    j =>
                    {
                        j.HasKey("CharacterId", "SessionId", "SkillId").HasName("player_skill_pkey");
                        j.ToTable("player_skill");
                        j.IndexerProperty<Guid>("CharacterId").HasColumnName("character_id");
                        j.IndexerProperty<Guid>("SessionId").HasColumnName("session_id");
                        j.IndexerProperty<int>("SkillId").HasColumnName("skill_id");
                    });
        });

        modelBuilder.Entity<Consumable>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("consumables_pkey");

            entity.ToTable("consumables");

            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnName("item_id");
            entity.Property(e => e.HpRestore)
                .HasDefaultValue(0)
                .HasColumnName("hp_restore");
            entity.Property(e => e.ManaRestore)
                .HasDefaultValue(0)
                .HasColumnName("mana_restore");

            entity.HasOne(d => d.Item).WithOne(p => p.Consumable)
                .HasForeignKey<Consumable>(d => d.ItemId)
                .HasConstraintName("consumables_item_id_fkey");
        });

        modelBuilder.Entity<Enemy>(entity =>
        {
            entity.HasKey(e => e.EnemyId).HasName("enemies_pkey");

            entity.ToTable("enemies");

            entity.Property(e => e.EnemyId)
                .HasMaxLength(10)
                .HasColumnName("enemy_id");
            entity.Property(e => e.Ad).HasColumnName("ad");
            entity.Property(e => e.Ap).HasColumnName("ap");
            entity.Property(e => e.AttackSpeed).HasColumnName("attack_speed");
            entity.Property(e => e.Def).HasColumnName("def");
            entity.Property(e => e.ExpReward).HasColumnName("exp_reward");
            entity.Property(e => e.Hp).HasColumnName("hp");
            entity.Property(e => e.IsRanged).HasColumnName("is_ranged");
            entity.Property(e => e.MovementSpeed).HasColumnName("movement_speed");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Res).HasColumnName("res");
            entity.Property(e => e.SpawnBiomeName)
                .HasMaxLength(20)
                .HasColumnName("spawn_biome_name");
            entity.Property(e => e.Tier)
                .HasMaxLength(10)
                .HasColumnName("tier");
        });

        modelBuilder.Entity<EnemyItem>(entity =>
        {
            entity.HasKey(e => new { e.EnemyId, e.ItemId }).HasName("enemy_item_pkey");

            entity.ToTable("enemy_item");

            entity.Property(e => e.EnemyId)
                .HasMaxLength(10)
                .HasColumnName("enemy_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.DropChance).HasColumnName("drop_chance");

            entity.HasOne(d => d.Enemy).WithMany(p => p.EnemyItems)
                .HasForeignKey(d => d.EnemyId)
                .HasConstraintName("enemy_item_enemy_id_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.EnemyItems)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("enemy_item_item_id_fkey");
        });

        modelBuilder.Entity<Gear>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("gears_pkey");

            entity.ToTable("gears");

            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnName("item_id");
            entity.Property(e => e.BonusAd)
                .HasDefaultValue(0)
                .HasColumnName("bonus_ad");
            entity.Property(e => e.BonusAp)
                .HasDefaultValue(0)
                .HasColumnName("bonus_ap");
            entity.Property(e => e.BonusAttackSpeed)
                .HasDefaultValueSql("0")
                .HasColumnName("bonus_attack_speed");
            entity.Property(e => e.BonusDef)
                .HasDefaultValue(0)
                .HasColumnName("bonus_def");
            entity.Property(e => e.BonusMovementSpeed)
                .HasDefaultValueSql("0")
                .HasColumnName("bonus_movement_speed");
            entity.Property(e => e.BonusRes)
                .HasDefaultValue(0)
                .HasColumnName("bonus_res");
            entity.Property(e => e.Effect)
                .HasColumnType("jsonb")
                .HasColumnName("effect");
            entity.Property(e => e.EquipSlotName)
                .HasMaxLength(50)
                .HasColumnName("equip_slot_name");

            entity.HasOne(d => d.Item).WithOne(p => p.Gear)
                .HasForeignKey<Gear>(d => d.ItemId)
                .HasConstraintName("gears_item_id_fkey");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("items_pkey");

            entity.ToTable("items");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.StackLimit)
                .HasDefaultValue((short)1)
                .HasColumnName("stack_limit");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.HasKey(e => e.Level1).HasName("levels_pkey");

            entity.ToTable("levels");

            entity.Property(e => e.Level1)
                .ValueGeneratedNever()
                .HasColumnName("level");
            entity.Property(e => e.Ad).HasColumnName("ad");
            entity.Property(e => e.Ap).HasColumnName("ap");
            entity.Property(e => e.AttackSpeed).HasColumnName("attack_speed");
            entity.Property(e => e.Def).HasColumnName("def");
            entity.Property(e => e.ExpNext).HasColumnName("exp_next");
            entity.Property(e => e.MovementSpeed).HasColumnName("movement_speed");
            entity.Property(e => e.Res).HasColumnName("res");
        });

        modelBuilder.Entity<PlayerItem>(entity =>
        {
            entity.HasKey(e => new { e.CharacterId, e.SessionId, e.ItemId }).HasName("player_item_pkey");

            entity.ToTable("player_item");

            entity.Property(e => e.CharacterId).HasColumnName("character_id");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue((short)1)
                .HasColumnName("quantity");

            entity.HasOne(d => d.Item).WithMany(p => p.PlayerItems)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("player_item_item_id_fkey");

            entity.HasOne(d => d.CharacterSession).WithMany(p => p.PlayerItems)
                .HasForeignKey(d => new { d.CharacterId, d.SessionId })
                .HasConstraintName("player_item_character_id_session_id_fkey");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("sessions_pkey");

            entity.ToTable("sessions");

            entity.Property(e => e.SessionId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("session_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.IsMultiplayer)
                .HasDefaultValue(false)
                .HasColumnName("is_multiplayer");
            entity.Property(e => e.LastLoad).HasColumnName("last_load");
            entity.Property(e => e.LastSave).HasColumnName("last_save");
            entity.Property(e => e.PlayTime)
                .HasDefaultValue(0)
                .HasColumnName("play_time");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.SkillId).HasName("skills_pkey");

            entity.ToTable("skills");

            entity.HasIndex(e => e.Name, "skills_name_key").IsUnique();

            entity.Property(e => e.SkillId).HasColumnName("skill_id");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Fullname, "users_fullname_key").IsUnique();

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .HasColumnName("fullname");
            entity.Property(e => e.IsBanned)
                .HasDefaultValue(false)
                .HasColumnName("is_banned");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValueSql("'player'::character varying")
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<WorldState>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("world_state_pkey");

            entity.ToTable("world_state");

            entity.Property(e => e.StateId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("state_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.EventId)
                .HasMaxLength(64)
                .HasColumnName("event_id");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.StateValue).HasColumnName("state_value");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Session).WithMany(p => p.WorldStates)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("world_state_session_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
