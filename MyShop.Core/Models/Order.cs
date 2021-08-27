using System.Collections.Generic;
using System.ComponentModel;

namespace MyShop.Core.Models
{
	public class Order : BaseEntity
	{
		public Order() {
			OrderItems = new List<OrderItem>();
		}

		[DisplayName("First Name")]
		public string FirstName { get; set; }

		[DisplayName("Last Name")]
		public string SurName { get; set; }

		public string Email { get; set; }

		public string Street { get; set; }

		public string City { get; set; }

		public string State { get; set; }

		public string OrderStatus { get; set; }

		public virtual ICollection<OrderItem> OrderItems { get; set; }
	}
}