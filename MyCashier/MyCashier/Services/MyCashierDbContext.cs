using Microsoft.EntityFrameworkCore;
using MyCashier.MVVM.Models;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace MyCashier.Services;

public partial class MyCashierDbContext : DbContext
{
    public static readonly MyCashierDbContext db = new();

    public virtual DbSet<Account> Account { get; set; }

    public virtual DbSet<Category> Category { get; set; }

    public virtual DbSet<Currency> Currency { get; set; }

    public virtual DbSet<Obligation> Obligation { get; set; }

    public virtual DbSet<ObligationStatus> ObligationStatus { get; set; }

    public virtual DbSet<ObligationType> ObligationType { get; set; }

    public virtual DbSet<Transaction> Transaction { get; set; }

    public virtual DbSet<TransactionType> TransactionType { get; set; }

    public virtual DbSet<User> User { get; set; }

    public MyCashierDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName,
                                               @"Resource\connectionstring.txt");
        if (File.Exists(connectionString))
        {
            optionsBuilder.UseNpgsql(File.ReadAllText(connectionString).TrimStart().TrimEnd());
        }
        else
        {
            MessageBox.Show("Ошибка строки содинения");
        }
    }


    #region redundant?
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Account>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("Account_pkey");

    //        entity.ToTable("Account");

    //        entity.HasIndex(e => e.Name, "Account_name_key").IsUnique();

    //        entity.Property(e => e.Id)
    //            .HasDefaultValueSql("gen_random_uuid()")
    //            .HasColumnName("id");
    //        entity.Property(e => e.Balance).HasColumnName("balance");
    //        entity.Property(e => e.CurrencyId)
    //            .HasMaxLength(3)
    //            .IsFixedLength()
    //            .HasColumnName("currencyID");
    //        entity.Property(e => e.Name)
    //            .HasMaxLength(50)
    //            .HasColumnName("name");
    //        entity.Property(e => e.UserId).HasColumnName("userID");

    //        entity.HasOne(d => d.Currency).WithMany(p => p.Accounts)
    //            .HasForeignKey(d => d.CurrencyId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("Account_currencyID_fkey");

    //        entity.HasOne(d => d.User).WithMany(p => p.Accounts)
    //            .HasForeignKey(d => d.UserId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("Account_userID_fkey");
    //    });

    //    modelBuilder.Entity<Category>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("Category_pkey");

    //        entity.ToTable("Category");

    //        entity.Property(e => e.Id)
    //            .HasDefaultValueSql("gen_random_uuid()")
    //            .HasColumnName("id");
    //        entity.Property(e => e.Icon).HasColumnName("icon");
    //        entity.Property(e => e.Name)
    //            .HasMaxLength(50)
    //            .HasColumnName("name");
    //    });

    //    modelBuilder.Entity<Currency>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("Currency_pkey");

    //        entity.ToTable("Currency");

    //        entity.Property(e => e.Id)
    //            .HasMaxLength(3)
    //            .IsFixedLength()
    //            .HasColumnName("id");
    //        entity.Property(e => e.Name)
    //            .HasMaxLength(50)
    //            .HasColumnName("name");
    //    });

    //    modelBuilder.Entity<Obligation>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("Obligation_pkey");

    //        entity.ToTable("Obligation");

    //        entity.HasIndex(e => e.Debtor, "Obligation_debtor_key").IsUnique();

    //        entity.Property(e => e.Id)
    //            .HasDefaultValueSql("gen_random_uuid()")
    //            .HasColumnName("id");
    //        entity.Property(e => e.AccountId).HasColumnName("accountID");
    //        entity.Property(e => e.CategoryId).HasColumnName("categoryID");
    //        entity.Property(e => e.Currency)
    //            .HasMaxLength(3)
    //            .IsFixedLength()
    //            .HasColumnName("currency");
    //        entity.Property(e => e.Date).HasColumnName("date");
    //        entity.Property(e => e.Debtor)
    //            .HasMaxLength(50)
    //            .HasColumnName("debtor");
    //        entity.Property(e => e.Description).HasColumnName("description");
    //        entity.Property(e => e.IsActive).HasColumnName("isActive");
    //        entity.Property(e => e.StatusId).HasColumnName("statusID");
    //        entity.Property(e => e.Sum).HasColumnName("sum");
    //        entity.Property(e => e.TypeId).HasColumnName("typeID");

    //        entity.HasOne(d => d.Account).WithMany(p => p.Obligations)
    //            .HasForeignKey(d => d.AccountId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("Obligation_accountID_fkey");

    //        entity.HasOne(d => d.Category).WithMany(p => p.Obligations)
    //            .HasForeignKey(d => d.CategoryId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("Obligation_categoryID_fkey");

    //        entity.HasOne(d => d.CurrencyNavigation).WithMany(p => p.Obligations)
    //            .HasForeignKey(d => d.Currency)
    //            .HasConstraintName("Obligation_currency_fkey");

    //        entity.HasOne(d => d.Status).WithMany(p => p.Obligations)
    //            .HasForeignKey(d => d.StatusId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("Obligation_statusID_fkey");

    //        entity.HasOne(d => d.Type).WithMany(p => p.Obligations)
    //            .HasForeignKey(d => d.TypeId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("Obligation_typeID_fkey");
    //    });

    //    modelBuilder.Entity<ObligationStatus>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("ObligationStatus_pkey");

    //        entity.ToTable("ObligationStatus");

    //        entity.HasIndex(e => e.Name, "ObligationStatus_name_key").IsUnique();

    //        entity.Property(e => e.Id)
    //            .HasDefaultValueSql("gen_random_uuid()")
    //            .HasColumnName("id");
    //        entity.Property(e => e.Name)
    //            .HasMaxLength(50)
    //            .HasColumnName("name");
    //    });

    //    modelBuilder.Entity<ObligationType>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("ObligationType_pkey");

    //        entity.ToTable("ObligationType");

    //        entity.HasIndex(e => e.Name, "ObligationType_name_key").IsUnique();

    //        entity.Property(e => e.Id)
    //            .HasDefaultValueSql("gen_random_uuid()")
    //            .HasColumnName("id");
    //        entity.Property(e => e.Name)
    //            .HasMaxLength(50)
    //            .HasColumnName("name");
    //    });

    //    modelBuilder.Entity<Transaction>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("Transaction_pkey");

    //        entity.ToTable("Transaction");

    //        entity.Property(e => e.Id)
    //            .HasDefaultValueSql("gen_random_uuid()")
    //            .HasColumnName("id");
    //        entity.Property(e => e.AccountId).HasColumnName("accountID");
    //        entity.Property(e => e.CategoryId).HasColumnName("categoryID");
    //        entity.Property(e => e.Currency)
    //            .HasMaxLength(3)
    //            .IsFixedLength()
    //            .HasColumnName("currency");
    //        entity.Property(e => e.Date).HasColumnName("date");
    //        entity.Property(e => e.Description).HasColumnName("description");
    //        entity.Property(e => e.Sum).HasColumnName("sum");
    //        entity.Property(e => e.TypeId).HasColumnName("typeID");

    //        entity.HasOne(d => d.Account).WithMany(p => p.Transactions)
    //            .HasForeignKey(d => d.AccountId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("Transaction_accountID_fkey");

    //        entity.HasOne(d => d.Category).WithMany(p => p.Transactions)
    //            .HasForeignKey(d => d.CategoryId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("Transaction_categoryID_fkey");

    //        entity.HasOne(d => d.CurrencyNavigation).WithMany(p => p.Transactions)
    //            .HasForeignKey(d => d.Currency)
    //            .HasConstraintName("Transaction_currency_fkey");

    //        entity.HasOne(d => d.Type).WithMany(p => p.Transactions)
    //            .HasForeignKey(d => d.TypeId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("Transaction_typeID_fkey");
    //    });

    //    modelBuilder.Entity<TransactionType>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("TransactionType_pkey");

    //        entity.ToTable("TransactionType");

    //        entity.HasIndex(e => e.Name, "TransactionType_name_key").IsUnique();

    //        entity.Property(e => e.Id)
    //            .HasDefaultValueSql("gen_random_uuid()")
    //            .HasColumnName("id");
    //        entity.Property(e => e.Name)
    //            .HasMaxLength(50)
    //            .HasColumnName("name");
    //    });

    //    modelBuilder.Entity<User>(entity =>
    //    {
    //        entity.HasKey(e => e.Id).HasName("User_pkey");

    //        entity.ToTable("User");

    //        entity.HasIndex(e => e.Login, "User_login_key").IsUnique();

    //        entity.Property(e => e.Id)
    //            .HasDefaultValueSql("gen_random_uuid()")
    //            .HasColumnName("id");
    //        entity.Property(e => e.Email)
    //            .HasMaxLength(50)
    //            .HasColumnName("email");
    //        entity.Property(e => e.Login)
    //            .HasMaxLength(30)
    //            .HasColumnName("login");
    //        entity.Property(e => e.Name)
    //            .HasMaxLength(50)
    //            .HasColumnName("name");
    //        entity.Property(e => e.Password)
    //            .HasMaxLength(30)
    //            .HasColumnName("password");
    //    });

    //    OnModelCreatingPartial(modelBuilder);
    //}

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    #endregion
}
