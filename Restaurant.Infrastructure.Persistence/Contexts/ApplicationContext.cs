using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Domain.Common;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}

        DbSet<Ingredient> Ingredients { get; set;}
        DbSet<Dish> Dishes { get; set;}
        DbSet<Order> Orders { get; set;}
        DbSet<Table> Tables { get; set;}

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.Modified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Tables
            modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
            modelBuilder.Entity<Dish>().ToTable("Dishes");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Table>().ToTable("Tables");
            #endregion

            #region Primary Keys
            modelBuilder.Entity<Ingredient>().HasKey(i => i.Id);
            modelBuilder.Entity<Dish>().HasKey(d => d.Id);
            modelBuilder.Entity<Order>().HasKey(o => o.Id); 
            modelBuilder.Entity<Table>().HasKey(t => t.Id);
            #endregion

            #region Relationships
            /*modelBuilder.Entity<Dish>()
                .HasMany<Ingredient>(d => d.Ingredients)
                .WithMany(i => i.Dishes)
                .UsingEntity(di => di.ToTable("DishesIngredients"));*/

            modelBuilder.Entity<Ingredient>()
                .HasMany(ingredient => ingredient.Dishes)
                .WithMany(dish => dish.Ingredients)
                .UsingEntity<DishIngredient>(
                    x => x
                        .HasOne(dishIngredient => dishIngredient.Dish)
                        .WithMany(ingredient => ingredient.DishIngredients)
                        .HasForeignKey(dishIngredient => dishIngredient.DishId),
                    x => x
                        .HasOne(dishIngredient => dishIngredient.Ingredient)
                        .WithMany(dish => dish.DishIngredients)
                        .HasForeignKey(dishIngredient => dishIngredient.IngredientId),
                    x =>
                    {
                        x.ToTable("DishIngredients");
                        x.HasKey(x => new { x.DishId, x.IngredientId });
                    }
                );

            modelBuilder.Entity<Order>()
                .HasMany(order => order.Dishes)
                .WithMany(dish => dish.Orders)
                .UsingEntity<DishOrder>(
                    x => x
                         .HasOne(dishOrder => dishOrder.Dish)
                         .WithMany(order => order.DishOrders)
                         .HasForeignKey(dishOrder => dishOrder.DishId),

                    x => x
                         .HasOne(dishOrder => dishOrder.Order)
                         .WithMany(dish => dish.DishOrders)
                         .HasForeignKey(dishOrder => dishOrder.OrderId),

                    x =>
                    {
                        x.ToTable("DishOrders");
                        x.HasKey(t => new { t.DishId, t.OrderId });
                    }
                );

            /*modelBuilder.Entity<Order>()
                .HasMany<Dish>(o => o.Dishes)
                .WithOne(d => d.Order)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.NoAction);*/

            modelBuilder.Entity<Table>()
                .HasMany<Order>(t => t.Orders)
                .WithOne(o => o.AssignedTable)
                .HasForeignKey(o => o.TableId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region "Property Configurations"
            #region Ingredients
            modelBuilder.Entity<Ingredient>()
                .Property(i => i.Name)
                .IsRequired();
            #endregion

            #region Dishes
            modelBuilder.Entity<Dish>()
                .Property(d => d.Name) 
                .IsRequired();

            modelBuilder.Entity<Dish>()
                .Property(d => d.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            #endregion

            #region Orders
            modelBuilder.Entity<Order>()
                .Property(o => o.Subtotal)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .IsRequired();  
            #endregion

            #region Tables
            modelBuilder.Entity<Table>()
                .Property(t => t.Status) 
                .IsRequired();
            #endregion
            #endregion
        }

    }
}
