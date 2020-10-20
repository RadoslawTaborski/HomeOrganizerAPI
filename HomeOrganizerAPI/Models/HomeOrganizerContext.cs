﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HomeOrganizerAPI.Models
{
    public partial class HomeOrganizerContext : DbContext
    {
        public HomeOrganizerContext()
        {
        }

        public HomeOrganizerContext(DbContextOptions<HomeOrganizerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Expenses> Expenses { get; set; }
        public virtual DbSet<ExpensesSettings> ExpensesSettings { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<PermanentItem> PermanentItem { get; set; }
        public virtual DbSet<Saldo> Saldo { get; set; }
        public virtual DbSet<ShoppingItem> ShoppingItem { get; set; }
        public virtual DbSet<ShoppingList> ShoppingList { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<Subcategory> Subcategory { get; set; }
        public virtual DbSet<TemporaryItem> TemporaryItem { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserGroups> UserGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.HasIndex(e => e.GroupId)
                    .HasName("fk_category_group1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Category)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_category_group1");
            });

            modelBuilder.Entity<Expenses>(entity =>
            {
                entity.ToTable("expenses");

                entity.HasIndex(e => e.GroupId)
                    .HasName("fk_expenses_group1_idx");

                entity.HasIndex(e => e.PayerId)
                    .HasName("fk_expenses_user1_idx");

                entity.HasIndex(e => e.RecipientId)
                    .HasName("fk_expenses_user2_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.PayerId)
                    .HasColumnName("payer_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RecipientId)
                    .HasColumnName("recipient_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("decimal(10,0)");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_expenses_group1");

                entity.HasOne(d => d.Payer)
                    .WithMany(p => p.ExpensesPayer)
                    .HasForeignKey(d => d.PayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_expenses_user1");

                entity.HasOne(d => d.Recipient)
                    .WithMany(p => p.ExpensesRecipient)
                    .HasForeignKey(d => d.RecipientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_expenses_user2");
            });

            modelBuilder.Entity<ExpensesSettings>(entity =>
            {
                entity.HasKey(e => new { e.User1Id, e.User2Id })
                    .HasName("PRIMARY");

                entity.ToTable("expenses_settings");

                entity.HasIndex(e => e.GroupId)
                    .HasName("fk_expenses_settings_group1_idx");

                entity.HasIndex(e => e.User1Id)
                    .HasName("fk_expenses_settings_user1_idx");

                entity.HasIndex(e => e.User2Id)
                    .HasName("fk_expenses_settings_user2_idx");

                entity.Property(e => e.User1Id)
                    .HasColumnName("user1_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.User2Id)
                    .HasColumnName("user2_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ExpensesSettings)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_expenses_settings_group1");

                entity.HasOne(d => d.User1)
                    .WithMany(p => p.ExpensesSettingsUser1)
                    .HasForeignKey(d => d.User1Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_expenses_settings_user1");

                entity.HasOne(d => d.User2)
                    .WithMany(p => p.ExpensesSettingsUser2)
                    .HasForeignKey(d => d.User2Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_expenses_settings_user2");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("group");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("item");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("fk_item_subcategory1_idx");

                entity.HasIndex(e => e.GroupId)
                    .HasName("fk_item_group1_idx");

                entity.HasIndex(e => e.ShoppingListId)
                    .HasName("fk_item_shopping_list1_idx");

                entity.HasIndex(e => e.StateId)
                    .HasName("fk_item_state1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Bought)
                    .HasColumnName("bought")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Counter)
                    .HasColumnName("counter")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ShoppingListId)
                    .HasColumnName("shopping_list_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StateId)
                    .HasColumnName("state_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_item_subcategory1");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_item_group1");

                entity.HasOne(d => d.ShoppingList)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.ShoppingListId)
                    .HasConstraintName("fk_item_shopping_list1");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("fk_item_state1");
            });

            modelBuilder.Entity<PermanentItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("permanent_item");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.StateId)
                    .HasColumnName("state_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Saldo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("saldo");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PayerId)
                    .HasColumnName("payer_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RecipientId)
                    .HasColumnName("recipient_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("decimal(10,0)");
            });

            modelBuilder.Entity<ShoppingItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("shopping_item");

                entity.Property(e => e.Archived)
                    .HasColumnName("archived")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Bought)
                    .HasColumnName("bought")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ShoppingListId)
                    .HasColumnName("shopping_list_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StateId)
                    .HasColumnName("state_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Visible)
                    .HasColumnName("visible")
                    .HasColumnType("tinyint(4)");
            });

            modelBuilder.Entity<ShoppingList>(entity =>
            {
                entity.ToTable("shopping_list");

                entity.HasIndex(e => e.GroupId)
                    .HasName("fk_shopping_list_group1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Visible)
                    .HasColumnName("visible")
                    .HasColumnType("tinyint(4)");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ShoppingList)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_shopping_list_group1");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("state");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Subcategory>(entity =>
            {
                entity.ToTable("subcategory");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("fk_subcategory_category1_idx");

                entity.HasIndex(e => e.GroupId)
                    .HasName("fk_subcategory_group1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Subcategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_subcategory_category1");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Subcategory)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_subcategory_group1");
            });

            modelBuilder.Entity<TemporaryItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("temporary_item");

                entity.Property(e => e.Bought)
                    .HasColumnName("bought")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ShoppingListId)
                    .HasColumnName("shopping_list_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserGroups>(entity =>
            {
                entity.ToTable("user_groups");

                entity.HasIndex(e => e.GroupId)
                    .HasName("fk_user_groups_group1_idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_user_groups_user1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_groups_group1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_groups_user1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
