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


                builder.HasMany(a => a.Customers).WithOne(c => c.Address);

                builder.HasMany(a => a.Orders).WithOne(o => o.Address);

                builder.HasOne(a => a.Store).WithOne(s => s.Address);
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

                builder.Property(c => c.Store)
                  .IsRequired();  // (column will be NOT NULL)


                builder.HasMany(c => c.Orders).WithOne(o => o.OrderedBy); // configuring the relationships is important
                                                                          // here we configure "both directions" of navigation property.
                                                                          // if we don't have an explicit foreign key property (e.g. OrderId)
                                                                          // that's perfectly fine (under the hood, a "shadow property" will
                                                                          // be made for it)
            });

            modelBuilder.Entity<Ingredient>(builder =>
            {
                builder.HasKey(i => i.Id);

                builder.Property(i => i.Name)
                        .IsRequired()  // (column will be NOT NULL)
                        .HasMaxLength(255); // (column will be NVARCHAR(255)


                builder.HasMany(i => i.PizzaIngredients).WithOne(pi => pi.Ingredient);

                builder.HasMany(i => i.StoreItems).WithOne(si => si.Ingredient);
            });

            modelBuilder.Entity<Order>(builder =>
            {
                builder.HasKey(o => o.Id);

                builder.Property(m => m.OrderTime) 
                    .IsRequired() // (column will be NOT NULL)
                    .HasColumnType("DATETIME2"); // (column will be DATETIME2)

                //total price?


                builder.HasOne(o => o.OrderedAt).WithMany(s => s.Orders);

                builder.HasMany(o => o.Items).WithOne(oi => oi.Order);

            });

            modelBuilder.Entity<OrderItem>(builder =>
            {
                builder.HasKey(oi => oi.Id);

                builder.Property(oi => oi.Quantity)
                    .IsRequired(); // (column will be NOT NULL)

                builder.Property(oi => oi.Pizza)
                    .IsRequired(); // (column will be NOT NULL)

                builder.Property(oi => oi.Order)
                    .IsRequired(); // (column will be NOT NULL)


                builder.HasOne(oi => oi.Pizza).WithMany(p => p.OrderItems);
            });

            modelBuilder.Entity<Pizza>(builder =>
            {
                builder.HasKey(p => p.Id);

                builder.Property(i => i.Name)
                       .IsRequired()  // (column will be NOT NULL)
                       .HasMaxLength(255); // (column will be NVARCHAR(255)


                builder.HasMany(p => p.PizzaIngredients).WithOne(pi => pi.Pizza);
            });

            modelBuilder.Entity<PizzaIngredient>(builder =>
            {
                builder.HasKey(pi => pi.Id);

                builder.Property(pi => pi.Quantity)
                    .IsRequired(); // (column will be NOT NULL)

                builder.Property(pi => pi.Pizza)
                    .IsRequired(); // (column will be NOT NULL)

                builder.Property(pi => pi.Ingredient)
                    .IsRequired(); // (column will be NOT NULL)
            });

            modelBuilder.Entity<Store>(builder =>
            {
                builder.HasKey(s => s.Id);

                builder.Property(s => s.Name)
                    .IsRequired()  // (column will be NOT NULL)
                    .HasMaxLength(255); // (column will be NVARCHAR(255)


                builder.HasMany(s => s.Inventory).WithOne(si => si.Store);
            });

            modelBuilder.Entity<StoreItem>(builder =>
            {
                builder.HasKey(si => si.Id);

                builder.Property(si => si.Quantity)
                    .IsRequired(); // (column will be NOT NULL)

                builder.Property(si => si.Ingredient)
                    .IsRequired(); // (column will be NOT NULL)

                builder.Property(si => si.Store)
                    .IsRequired();  // (column will be NOT NULL)
            });
        }

    }
}
