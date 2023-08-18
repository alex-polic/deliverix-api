using Deliverix.Common.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Deliverix.DAL.Models.Contexts;

public class DeliverixContext : DbContext
{
    public DeliverixContext()
    {
        
    }
    
    public DeliverixContext(DbContextOptions<DeliverixContext> options)
        : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderedProduct> OrderedProducts { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(AppConfiguration.GetConfiguration("Entities", "ConnectionStrings"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Username)
                .HasMaxLength(256)
                .IsRequired();

            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .IsRequired();

            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .IsRequired();

            entity.Property(e => e.FullName)
                .HasMaxLength(256)
                .IsRequired();

            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

            entity.Property(e => e.Address)
                .HasMaxLength(256)
                .IsRequired();

            entity.Property(e => e.UserType)
                .IsRequired();

            entity.Property(e => e.ProfilePictureUrl)
                .HasMaxLength(256)
                .IsRequired();

            entity.Property(e => e.VerificationStatus)
                .IsRequired();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });
        
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(256)
                .IsRequired();

            entity.Property(e => e.Comment)
                .HasMaxLength(256)
                .IsRequired();

            entity.Property(e => e.FullPrice).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.DeliveryStatus)
                .HasMaxLength(256)
                .IsRequired();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            
            entity.Property(e => e.DeliveryDateTime).IsRequired(false).HasColumnType("datetime");
            
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            
            entity.HasOne(o => o.Buyer)
                .WithMany(u => u.BuyerOrders)
                .HasForeignKey(d => d.BuyerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Buyer_Orders");
            
            entity.HasOne(o => o.Seller)
                .WithMany(u => u.SellerOrders)
                .HasForeignKey(d => d.SellerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Seller_Orders");
        });
        
        modelBuilder.Entity<OrderedProduct>(entity =>
        {
            entity.ToTable("OrderedProduct");

            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            
            entity.HasOne(o => o.Order)
                .WithMany(u => u.OrderedProducts)
                .HasForeignKey(d => d.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_OrderProducts_Orders");
            
            entity.HasOne(o => o.Product)
                .WithMany(u => u.OrderedProducts)
                .HasForeignKey(d => d.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_OrderedProducts_Products");
        });
        
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .IsRequired();

            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();
            
            entity.Property(e => e.IngredientsDescription)
                .HasMaxLength(1024)
                .IsRequired();
            
        });
    }
}