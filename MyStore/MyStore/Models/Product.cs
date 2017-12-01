using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class Product
    {
		public Product()
		{

		}

		public int Id { get; set; }
		public string Title { get; set; }
		public string Info { get; set; }
		public decimal Price { get; set; }
	}
}
