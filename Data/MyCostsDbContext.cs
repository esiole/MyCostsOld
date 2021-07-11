using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCosts.Models;

namespace MyCosts.Data
{
    //public class MyCostsContext : DbContext
    public class MyCostsDbContext : IdentityDbContext<User>
    {
        public MyCostsDbContext()
        {
        }

        public MyCostsDbContext(DbContextOptions<MyCostsDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public virtual DbSet<Cost> Costs { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        //public virtual DbSet<Role> Roles { get; set; }
        //public virtual DbSet<UserRole> UserRoles { get; set; }
        //public virtual DbSet<User> Users { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-TAASPD0;Database=MyCosts;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cost>(entity =>
            {
                //entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Store).HasMaxLength(50);

                entity.Property(e => e.Sum).HasColumnType("money");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Costs)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Costs_To_Products");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Costs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Costs_To_Users");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                //entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                //entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Products_To_Categories");
            });

            //modelBuilder.Entity<Role>(entity =>
            //{
            //    entity.HasIndex(e => e.Name)
            //        .HasName("UQ__Roles__737584F6E3582C0D")
            //        .IsUnique();

            //    entity.Property(e => e.Name)
            //        .IsRequired()
            //        .HasMaxLength(50)
            //        .IsUnicode(false);
            //});

            //modelBuilder.Entity<UserRole>(entity =>
            //{
            //    //entity.Property(e => e.Id).ValueGeneratedNever();

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.UserRoles)
            //        .HasForeignKey(d => d.RoleId)
            //        .HasConstraintName("FK_UserRoles_To_Roles");

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.UserRoles)
            //        .HasForeignKey(d => d.UserId)
            //        .HasConstraintName("FK_UserRoles_To_Users");
            //});

            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.HasIndex(e => e.Email)
            //        .HasName("UQ__Users__A9D105345825CA07")
            //        .IsUnique();

            //    entity.Property(e => e.Email)
            //        .IsRequired()
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    //entity.Property(e => e.Password)
            //    //    .IsRequired()
            //    //    .HasMaxLength(50)
            //    //    .IsUnicode(false);
            //});
        }
    }
}
