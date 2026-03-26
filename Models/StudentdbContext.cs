using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using mytheme.Models;

namespace mytheme.Models;

public partial class StudentdbContext : DbContext
{
    public StudentdbContext()
    {
    }

    public StudentdbContext(DbContextOptions<StudentdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categ> Categs { get; set; }

    public virtual DbSet<CusCard> CusCards { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Studatum> Studata { get; set; }

    public virtual DbSet<Stumark> Stumarks { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=.;initial catalog=Studentdb;user id=sa;password=aptech; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categ>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categ__3214EC071C997AC1");

            entity.ToTable("Categ");

            entity.Property(e => e.Namee)
                .HasMaxLength(250)
                .HasColumnName("namee");
        });

        modelBuilder.Entity<CusCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07DA795E6C");

            entity.ToTable("cus_card");

            entity.Property(e => e.CreatedAt).HasMaxLength(50);
            entity.Property(e => e.FinalImagePath).HasMaxLength(50);
            entity.Property(e => e.Message).HasMaxLength(50);
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Template).WithMany(p => p.CusCards)
                .HasForeignKey(d => d.TemplateId)
                .HasConstraintName("FK_cus_card_ToTable");

            entity.HasOne(d => d.User).WithMany(p => p.CusCards)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK_cus_card_ToTable_1");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Login__3214EC07727D90C3");

            entity.ToTable("Login");

            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .HasColumnName("email");
            entity.Property(e => e.Passwordd)
                .HasMaxLength(250)
                .HasColumnName("passwordd");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(250)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Logins)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Login_ToTable");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("product");

            entity.Property(e => e.CatId).HasColumnName("cat_id");
            entity.Property(e => e.Descp)
                .HasMaxLength(50)
                .HasColumnName("descp");
            entity.Property(e => e.Namee)
                .HasMaxLength(50)
                .HasColumnName("namee");
            entity.Property(e => e.Picture)
                .HasMaxLength(200)
                .HasColumnName("picture");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Qty).HasColumnName("qty");

            entity.HasOne(d => d.Cat).WithMany(p => p.Products)
                .HasForeignKey(d => d.CatId)
                .HasConstraintName("FK_product_ToTable");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07F6730204");

            entity.ToTable("Role");

            entity.Property(e => e.Namee).HasMaxLength(250);
        });

        modelBuilder.Entity<Studatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__studata__3214EC07787BA0C7");

            entity.ToTable("studata");

            entity.Property(e => e.Addresss)
                .HasMaxLength(250)
                .HasColumnName("addresss");
            entity.Property(e => e.Fee).HasColumnName("fee");
            entity.Property(e => e.Stuname)
                .HasMaxLength(250)
                .HasColumnName("stuname");
        });

        modelBuilder.Entity<Stumark>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stumarks__3214EC07B888E15A");

            entity.Property(e => e.Marks).HasColumnName("marks");
            entity.Property(e => e.Stuid).HasColumnName("stuid");

            entity.HasOne(d => d.Stu).WithMany(p => p.Stumarks)
                .HasForeignKey(d => d.Stuid)
                .HasConstraintName("FK_Stumarks_ToTable");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User_det__3214EC07CA6F7543");

            entity.ToTable("User_details");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Departr)
                .HasMaxLength(50)
                .HasColumnName("departr");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_User_details_ToTable_1");

            entity.HasOne(d => d.User).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_User_details_ToTable");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<mytheme.Models.PurchasesViewModel> PurchasesViewModel { get; set; } = default!;
}
