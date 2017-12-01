using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models.ViewModels
{
	public class ViewModelProduct
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Info { get; set; }
		public decimal Price { get; set; }
	}

	public class ViewModelProducts
    {
		public List<ViewModelProduct> Products { get; set; }
	}
}
