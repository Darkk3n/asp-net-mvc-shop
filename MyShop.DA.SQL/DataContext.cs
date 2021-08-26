﻿using System.Data.Entity;
using MyShop.Core.Models;

namespace MyShop.DA.SQL
{
	public class DataContext : DbContext
	{
		public DataContext()
			: base("ShopConn") {

		}

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }
	}
}