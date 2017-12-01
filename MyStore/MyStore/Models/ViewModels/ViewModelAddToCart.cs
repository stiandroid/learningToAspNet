namespace MyStore.Models.ViewModels
{
	public class ViewModelAddToCart
    {
		// TODO: Refactor this. Can this be merged/replaced with ViewModelShoppingCart.cs?
		// Product
		public int ProductId { get; set; }
		public string ProductTitle { get; set; }
		public string ProductInfo { get; set; }
		public decimal ProductPrice { get; set; }

		// ShoppingCart
		public string ShoppingCartSessionId { get; set; }
		// (not in use yet) public string ShoppingCartTitle { get; set; }
		// (not in use yet) public int ShoppingCartCustomerId { get; set; }

		// ShoppingCartItem
		public int ShoppingCartItemProductId { get; set; }
		public int ShoppingCartItemQuantity { get; set; }
		public decimal LinePrice
		{
			get
			{
				return ShoppingCartItemQuantity * ProductPrice;
			}
		}

	}
}
