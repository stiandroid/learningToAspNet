using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class ShoppingCart
    {
		public ShoppingCart()
		{

		}

		public int Id { get; set; }
		public string Title { get; set; }
		public string SessionId { get; set; }
		public int CustomerID { get; set; }
		[DataType(DataType.Date)]
		public DateTime CreateDate { get; set; }
	}
}
