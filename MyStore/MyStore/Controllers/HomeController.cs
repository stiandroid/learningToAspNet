using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyStore.Models;
using MyStore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;

namespace MyStore.Controllers
{
	public class HomeController : Controller
	{
		private readonly MyStoreContext _context;

		public HomeController(MyStoreContext context)
		{
			_context = context;
		}

		// GET: Products
		public async Task<IActionResult> Index()
		{
			return View(await _context.Products.ToListAsync());
		}

		// GET: Products/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _context.Products
				.SingleOrDefaultAsync(m => m.Id == id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// POST: Home/AddToCart
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddToCart(
				[Bind("ShoppingCartItemProductId,ShoppingCartItemQuantity")]
				ViewModelAddToCart model)
		{
			// Initialize session to enable SessionId
			// TODO: Sjekke om det er nødvendig å sette navnet hver gang. Kanskje man kan sjekke om cookien finnes først?
			HttpContext.Session.SetString("_Name", "MyStore");

			DateTime dt = DateTime.Now;
			string SessionId = HttpContext.Session.Id;

			var ShoppingCart = new ShoppingCart()
			{
				SessionId = SessionId,
				CreateDate = dt,
				Title = "My shopping cart (" + dt.ToShortDateString() + ")"
				// (not in use yet) CustomerId = 0
			};

			var ShoppingCartItem = new ShoppingCartItem()
			{
				ProductId = model.ShoppingCartItemProductId,
				Quantity = model.ShoppingCartItemQuantity
			};

			// 1. If a ShoppingCart exist with current SessionId, get ShoppingCartId from
			// that one and use it in the ShoppingCartItem
			if (ModelState.IsValid)
			{
				// Query for ShoppingCart containing current SessionId.
				var cartInfo =
					(from Cart in _context.ShoppingCarts
					 where Cart.SessionId == SessionId
					 select new { TempId = Cart.Id })
						.SingleOrDefault();
				if (cartInfo != null)
				{
					// *** [ Use existing ShoppingCart ] ***
					// 2. Set ShoppingCartId for ShoppingCartItem
					ShoppingCartItem.ShoppingCartId = cartInfo.TempId;
				}
				else
				{
					// *** [ Create a new shoppingCart ] ***
					_context.ShoppingCarts.Add(ShoppingCart);
					await _context.SaveChangesAsync();

					// 2. Set ShoppingCartId in ShoppingCartItem
					ShoppingCartItem.ShoppingCartId = ShoppingCart.Id;
				}

				// 4. save or update shoppingCartItem
				// Query for ShoppingCartItem containing current ProductId
				var cartItemInfo =
					(from CartItem in _context.ShoppingCartItems
					 where CartItem.ProductId == model.ShoppingCartItemProductId
					 select new { TempQty = CartItem.Quantity, TempId = CartItem.Id })
						.FirstOrDefault();
				if (cartItemInfo != null)
				{
					// 5. update quantity for existing ShoppingCartItem
					ShoppingCartItem.Id = cartItemInfo.TempId;
					ShoppingCartItem.Quantity += cartItemInfo.TempQty; // current quantity + added quantity
					_context.ShoppingCartItems.Update(ShoppingCartItem);
				}
				else
				{
					// 5. create a new shoppingCartItem
					_context.ShoppingCartItems.Add(ShoppingCartItem);
				}
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Home");
			}
			return View("Index", "Home");
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
