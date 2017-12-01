using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class ShoppingCartItem
    {
		public ShoppingCartItem()
		{

		}

		public int Id { get; set; }
		public int ShoppingCartId { get; set; }
		public int ProductId { get; set; }
		[Range(0, int.MaxValue)]
		public int Quantity { get; set; }
	}
}
