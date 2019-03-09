using Microsoft.EntityFrameworkCore;
using Project1.Library;

namespace Project1.DataAccess
{
    public class Project1DbContext : DbContext
    {
        public Project1DbContext()
        { }

        public Project1DbContext(DbContextOptions<Project1DbContext> options) : base(options)
        { }

        public DbSet<Address> Address { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Pizza> Pizza { get; set; }
        public DbSet<PizzaIngredient> PizzaIngredient { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<StoreItem> StoreItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(builder =>
            {
                builder.HasKey(a => a.Id);

                builder.Property(a => a.Street)
                       .IsRequired()  // (column will be NOT NULL)
                       .HasMaxLength(255); // (column will be NVARCHAR(255)

                builder.Property(a => a.Country)
                      .IsRequired()  // (column will be NOT NULL)
                      .HasMaxLength(255); // (column will be NVARCHAR(255)


                builder.HasMany(a => a.Customers).WithOne(c => c.Address).OnDelete(DeleteBehavior.SetNull); //customer address is nullable

                builder.HasMany(a => a.Orders).WithOne(o => o.Address).OnDelete(DeleteBehavior.Restrict); //prevent deleting order history

                builder.HasOne(a => a.Store).WithOne(s => s.Address).HasForeignKey("Store", "AddressId").OnDelete(DeleteBehavior.Restrict);
                //Store will have shadow property AddressId foreign key
            });

            modelBuilder.Entity<Customer>(builder =>
            {
                builder.HasKey(c => c.Id);

                builder.Property(c => c.FirstName)
                       .IsRequired()  // (column will be NOT NULL)
                       .HasMaxLength(255); // (column will be NVARCHAR(255)

                builder.Property(c => c.LastName)
                  .IsRequired()  // (column will be NOT NULL)
                  .HasMaxLength(255); // (column will be NVARCHAR(255)


                builder.HasMany(c => c.Orders).WithOne(o => o.OrderedBy).OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(c => c.Store).WithMany(s => s.Customers).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Ingredient>(builder =>
            {
                builder.HasKey(i => i.Id);

                builder.Property(i => i.Name)
                        .IsRequired()  // (column will be NOT NULL)
                        .HasMaxLength(255); // (column will be NVARCHAR(255)


                builder.HasMany(i => i.PizzaIngredients).WithOne(pi => pi.Ingredient).OnDelete(DeleteBehavior.Restrict);

                builder.HasMany(i => i.StoreItems).WithOne(si => si.Ingredient).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Order>(builder =>
            {
                builder.HasKey(o => o.Id);

                builder.Property(o => o.OrderTime)
                    .IsRequired() // (column will be NOT NULL)
                    .HasColumnType("DATETIME2"); // (column will be DATETIME2)

                builder.Property(o => o.TotalPrice).HasColumnType("decimal(8, 2)");


                builder.HasOne(o => o.OrderedAt).WithMany(s => s.Orders).OnDelete(DeleteBehavior.Restrict);

                builder.HasMany(o => o.OrderItems).WithOne(oi => oi.Order).OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<OrderItem>(builder =>
            {
                builder.HasKey(oi => oi.Id);

                builder.Property(oi => oi.Quantity)
                    .IsRequired(); // (column will be NOT NULL)


                builder.HasOne(oi => oi.Pizza).WithMany(p => p.OrderItems).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Pizza>(builder =>
            {
                builder.HasKey(p => p.Id);

                builder.Property(i => i.Name)
                       .IsRequired()  // (column will be NOT NULL)
                       .HasMaxLength(255); // (column will be NVARCHAR(255)

                builder.Property(p => p.Price).HasColumnType("decimal(6, 2)");


                builder.HasMany(p => p.PizzaIngredients).WithOne(pi => pi.Pizza).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PizzaIngredient>(builder =>
            {
                builder.HasKey(pi => pi.Id);

                builder.Property(pi => pi.Quantity)
                    .IsRequired(); // (column will be NOT NULL)

            });

            modelBuilder.Entity<Store>(builder =>
            {
                builder.HasKey(s => s.Id);

                builder.Property(s => s.Name)
                    .IsRequired()  // (column will be NOT NULL)
                    .HasMaxLength(255); // (column will be NVARCHAR(255)


                builder.HasMany(s => s.StoreItems).WithOne(si => si.Store).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<StoreItem>(builder =>
            {
                builder.HasKey(si => si.Id);

                builder.Property(si => si.Quantity)
                    .IsRequired(); // (column will be NOT NULL)
            });

        }

    }
}
