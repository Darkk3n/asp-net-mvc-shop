using System;
using System.ComponentModel;

namespace MyShop.Core.Models
{
	public abstract class BaseEntity
	{
		public string Id { get; set; }
		[DisplayName("Created At")]
		public DateTimeOffset CreatedAt { get; set; }

		public BaseEntity() {
			this.Id = Guid.NewGuid().ToString();
			this.CreatedAt = DateTime.Now;
		}
	}
}
