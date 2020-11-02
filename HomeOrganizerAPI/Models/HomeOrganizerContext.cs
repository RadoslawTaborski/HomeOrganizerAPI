using System;
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
        public virtual DbSet<ExpenseDetails> ExpenseDetails { get; set; }
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
                entity.HasKey(e => e.Uuid)
                    .HasName("PRIMARY");

                entity.ToTable("category");

                entity.HasIndex(e => e.GroupUuid)
                    .HasName("fk_category_group1_idx");

                entity.HasIndex(e => e.Uuid)
                    .HasName("uuid")
                    .IsUnique();

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupUuid)
                    .IsRequired()
                    .HasColumnName("group_uuid")
                    .HasColumnType("binary(16)");

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
                    .HasForeignKey(d => d.GroupUuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_category_group1");
            });

            modelBuilder.Entity<ExpenseDetails>(entity =>
            {
                entity.HasKey(e => e.Uuid)
                    .HasName("PRIMARY");

                entity.ToTable("expense_details");

                entity.HasIndex(e => e.ExpenseUuid)
                    .HasName("fk_expense_details_expenses1_idx");

                entity.HasIndex(e => e.PayerUuid)
                    .HasName("fk_expense_details_user2_idx");

                entity.HasIndex(e => e.RecipientUuid)
                    .HasName("fk_expense_details_user1_idx");

                entity.HasIndex(e => e.Uuid)
                    .HasName("uuid")
                    .IsUnique();

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ExpenseUuid)
                    .IsRequired()
                    .HasColumnName("expense_uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.PayerUuid)
                    .IsRequired()
                    .HasColumnName("payer_uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.RecipientUuid)
                    .IsRequired()
                    .HasColumnName("recipient_uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("decimal(10,3)");

                entity.HasOne(d => d.Expense)
                    .WithMany(p => p.ExpenseDetails)
                    .HasForeignKey(d => d.ExpenseUuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_expense_details_expenses1");

                entity.HasOne(d => d.Payer)
                    .WithMany(p => p.ExpenseDetailsPayer)
                    .HasForeignKey(d => d.PayerUuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_expense_details_user2");

                entity.HasOne(d => d.Recipient)
                    .WithMany(p => p.ExpenseDetailsRecipient)
                    .HasForeignKey(d => d.RecipientUuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_expense_details_user1");
            });

            modelBuilder.Entity<Expenses>(entity =>
            {
                entity.HasKey(e => e.Uuid)
                    .HasName("PRIMARY");

                entity.ToTable("expenses");

                entity.HasIndex(e => e.GroupUuid)
                    .HasName("fk_expenses_group1_idx");

                entity.HasIndex(e => e.Uuid)
                    .HasName("uuid")
                    .IsUnique();

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupUuid)
                    .IsRequired()
                    .HasColumnName("group_uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.GroupUuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_expenses_group1");
            });

            modelBuilder.Entity<ExpensesSettings>(entity =>
            {
                entity.HasKey(e => e.Uuid)
                    .HasName("PRIMARY");

                entity.ToTable("expenses_settings");

                entity.HasIndex(e => e.UserGroupsUuid)
                    .HasName("fk_expenses_settings_user_groups1_idx");

                entity.HasIndex(e => e.Uuid)
                    .HasName("uuid")
                    .IsUnique();

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UserGroupsUuid)
                    .IsRequired()
                    .HasColumnName("user_groups_uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.UserGroups)
                    .WithMany(p => p.ExpensesSettings)
                    .HasForeignKey(d => d.UserGroupsUuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_expenses_settings_user_groups1");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.Uuid)
                    .HasName("PRIMARY");

                entity.ToTable("group");

                entity.HasIndex(e => e.Uuid)
                    .HasName("uuid")
                    .IsUnique();

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasColumnType("binary(16)");

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
                entity.HasKey(e => e.Uuid)
                    .HasName("PRIMARY");

                entity.ToTable("item");

                entity.HasIndex(e => e.CategoryUuid)
                    .HasName("fk_item_subcategory1_idx");

                entity.HasIndex(e => e.GroupUuid)
                    .HasName("fk_item_group1_idx");

                entity.HasIndex(e => e.ShoppingListUuid)
                    .HasName("fk_item_shopping_list1_idx");

                entity.HasIndex(e => e.StateUuid)
                    .HasName("fk_item_state1_idx");

                entity.HasIndex(e => e.Uuid)
                    .HasName("uuid")
                    .IsUnique();

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.Bought)
                    .HasColumnName("bought")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CategoryUuid)
                    .IsRequired()
                    .HasColumnName("category_uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.Counter)
                    .HasColumnName("counter")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupUuid)
                    .IsRequired()
                    .HasColumnName("group_uuid")
                    .HasColumnType("binary(16)");

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

                entity.Property(e => e.ShoppingListUuid)
                    .HasColumnName("shopping_list_uuid")
                    .HasColumnType("binary(16)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StateUuid)
                    .HasColumnName("state_uuid")
                    .HasColumnType("binary(16)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.CategoryUuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_item_subcategory1");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.GroupUuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_item_group1");

                entity.HasOne(d => d.ShoppingList)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.ShoppingListUuid)
                    .HasConstraintName("fk_item_shopping_list1");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.StateUuid)
                    .HasConstraintName("fk_item_state1");
            });

            modelBuilder.Entity<PermanentItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("permanent_item");

                entity.Property(e => e.CategoryUuid)
                    .IsRequired()
                    .HasColumnName("category_uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.Counter)
                    .HasColumnName("counter")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupUuid)
                    .IsRequired()
                    .HasColumnName("group_uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.StateUuid)
                    .HasColumnName("state_uuid")
                    .HasColumnType("binary(16)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Uuid)
                    .IsRequired()
                    .HasColumnName("uuid")
                    .HasColumnType("binary(16)");
            });

            modelBuilder.Entity<Saldo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("saldo");

                entity.Property(e => e.GroupUuid)
                    .IsRequired()
                    .HasColumnName("group_uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.PayerUuid)
                    .IsRequired()
                    .HasColumnName("payer_uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.RecipientUuid)
                    .IsRequired()
                    .HasColumnName("recipient_uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("decimal(33,3)")
                    .HasDefaultValueSql("'NULL'");
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

                entity.Property(e => e.CategoryUuid)
                    .IsRequired()
                    .HasColumnName("category_uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.Counter)
                    .HasColumnName("counter")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupUuid)
                    .IsRequired()
                    .HasColumnName("group_uuid")
                    .HasColumnType("binary(16)");

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

                entity.Property(e => e.ShoppingListUuid)
                    .HasColumnName("shopping_list_uuid")
                    .HasColumnType("binary(16)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StateUuid)
                    .HasColumnName("state_uuid")
                    .HasColumnType("binary(16)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Uuid)
                    .IsRequired()
                    .HasColumnName("uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.Visible)
                    .HasColumnName("visible")
                    .HasColumnType("tinyint(1)");
            });

            modelBuilder.Entity<ShoppingList>(entity =>
            {
                entity.HasKey(e => e.Uuid)
                    .HasName("PRIMARY");

                entity.ToTable("shopping_list");

                entity.HasIndex(e => e.GroupUuid)
                    .HasName("fk_shopping_list_group1_idx");

                entity.HasIndex(e => e.Uuid)
                    .HasName("uuid")
                    .IsUnique();

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasColumnType("binary(16)");

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

                entity.Property(e => e.GroupUuid)
                    .IsRequired()
                    .HasColumnName("group_uuid")
                    .HasColumnType("binary(16)");

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
                    .HasColumnType("tinyint(1)");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ShoppingList)
                    .HasForeignKey(d => d.GroupUuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_shopping_list_group1");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.Uuid)
                    .HasName("PRIMARY");

                entity.ToTable("state");

                entity.HasIndex(e => e.Level)
                    .HasName("level")
                    .IsUnique();

                entity.HasIndex(e => e.Uuid)
                    .HasName("uuid")
                    .IsUnique();

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("int(11)");

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
                entity.HasKey(e => e.Uuid)
                    .HasName("PRIMARY");

                entity.ToTable("subcategory");

                entity.HasIndex(e => e.CategoryUuid)
                    .HasName("fk_subcategory_category1_idx");

                entity.HasIndex(e => e.GroupUuid)
                    .HasName("fk_subcategory_group1_idx");

                entity.HasIndex(e => e.Uuid)
                    .HasName("uuid")
                    .IsUnique();

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.CategoryUuid)
                    .IsRequired()
                    .HasColumnName("category_uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupUuid)
                    .IsRequired()
                    .HasColumnName("group_uuid")
                    .HasColumnType("binary(16)");

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
                    .HasForeignKey(d => d.CategoryUuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_subcategory_category1");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Subcategory)
                    .HasForeignKey(d => d.GroupUuid)
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

                entity.Property(e => e.CategoryUuid)
                    .IsRequired()
                    .HasColumnName("category_uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupUuid)
                    .IsRequired()
                    .HasColumnName("group_uuid")
                    .HasColumnType("binary(16)");

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

                entity.Property(e => e.ShoppingListUuid)
                    .HasColumnName("shopping_list_uuid")
                    .HasColumnType("binary(16)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Uuid)
                    .IsRequired()
                    .HasColumnName("uuid")
                    .HasColumnType("binary(16)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Uuid)
                    .HasName("PRIMARY");

                entity.ToTable("user");

                entity.HasIndex(e => e.Uuid)
                    .HasName("uuid")
                    .IsUnique();

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasColumnType("binary(16)");

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
                entity.HasKey(e => e.Uuid)
                    .HasName("PRIMARY");

                entity.ToTable("user_groups");

                entity.HasIndex(e => e.GroupUuid)
                    .HasName("fk_user_groups_group1_idx");

                entity.HasIndex(e => e.UserUuid)
                    .HasName("fk_user_groups_user1_idx");

                entity.HasIndex(e => e.Uuid)
                    .HasName("uuid")
                    .IsUnique();

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.DeleteTime)
                    .HasColumnName("delete_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupUuid)
                    .IsRequired()
                    .HasColumnName("group_uuid")
                    .HasColumnType("binary(16)");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UserUuid)
                    .IsRequired()
                    .HasColumnName("user_uuid")
                    .HasColumnType("binary(16)");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.GroupUuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_groups_group1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.UserUuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_groups_user1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

