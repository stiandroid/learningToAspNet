using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models.ViewModels
{
    public class ViewModelShoppingCart
    {
		public string Title { get; set; }
		[DataType(DataType.Date)]
		public DateTime CreateDate { get; set; }
		public List<ViewModelShoppingCartItem> ShoppingCartItems { get; set; }
		public decimal TotalSum => ShoppingCartItems.Sum(item => item.LineSum);
	}

	public class ViewModelShoppingCartItem
	{
		public int ProductId { get; set; }
		public string ProductTitle { get; set; }
		public decimal ProductPrice { get; set; }
		public int Quantity { get; set; }
		public decimal LineSum => ProductPrice * Quantity;
	}
}
