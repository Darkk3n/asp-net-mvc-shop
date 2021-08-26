using System.ComponentModel;

namespace MyShop.Core.Models
{
	public class ProductCategory : BaseEntity
	{
		[DisplayName("Category Name")]
		public string CategoryName { get; set; }
	}
}