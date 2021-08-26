using System.Collections.Generic;
using MyShop.Core.Models;

namespace MyShop.Core.ViewModels
{
	public class ProductManagerViewModel
	{
		public Product Product { get; set; }
		public IEnumerable<ProductCategory> Categories { get; set; }
	}
}