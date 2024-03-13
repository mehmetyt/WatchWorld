using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace Infrastructure.Data
{
	public class WatchWorldContext : DbContext
	{
		public WatchWorldContext(DbContextOptions<WatchWorldContext> options) : base(options) 
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Basket> Baskets { get; set; }
		public DbSet<BasketItem> BasketItems { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrdersItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

	}
}
