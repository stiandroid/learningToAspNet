using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class MyStoreContext : DbContext
    {
		public MyStoreContext(DbContextOptions options) : base(options) { }

		public DbSet<Product> Products { get; set; }
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
	}
}
