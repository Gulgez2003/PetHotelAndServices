using Stripe;

namespace FinalProjectApp.Presentation.Controllers
{
    public class CartController : Controller
    {
        public const string COOKIES_BASKET = "cart";
        private readonly ISender _sender;

        public CartController(ISender sender)
        {
            _sender = sender;
        }

        public async Task<IActionResult> Index()
        {
            List<CartViewModel> cartViewModels = GetCartViewModels();
            List<CartItemViewModel> cartItemViewModels = new List<CartItemViewModel>();
            foreach (var item in cartViewModels)
            {
                var product = await _sender.Send(new GetProductByIdQuery { Id = item.ProductId });
                CartItemViewModel cartItemViewModel = new CartItemViewModel()
                {
                    Id = item.ProductId,
                    ProductCount = item.Count,
                    Title = product.Title,
                    Price = product.Price,
                    Colours = product.Colours,
                    ProductImages = product.ProductImages,
                    InStock = product.InStock
                };
                cartItemViewModels.Add(cartItemViewModel);
            }

            return View(cartItemViewModels);
        }

        public async Task<IActionResult> AddToCart(int productId)
        {
            if (productId == 0)
            {
                return NotFound();
            }
            var dbProduct = await _sender.Send(new GetProductByIdQuery { Id = productId });
            if (dbProduct == null)
            {
                return BadRequest();
            }

            List<CartViewModel> cartViewModels = GetCartViewModels();
            CheckCartViewModel(productId, cartViewModels);

            UpdateCookie(cartViewModels);

            return RedirectToAction("Index", "Products");
        }

        private void UpdateCookie(List<CartViewModel> cartViewModels)
        {
            string cookiesCart = JsonConvert.SerializeObject(cartViewModels);
            Response.Cookies.Append(COOKIES_BASKET, cookiesCart);
        }

        private void CheckCartViewModel(int id, List<CartViewModel> cartViewModels)
        {
            CartViewModel existCartItem = cartViewModels.FirstOrDefault(b => b.ProductId == id);
            if (existCartItem != null)
            {
                existCartItem.Count++;
            }
            else
            {
                cartViewModels.Add(new CartViewModel { ProductId = id, Count = 1 });
            }
        }

        private List<CartViewModel> GetCartViewModels()
        {
            List<CartViewModel> cartViewModels;

            if (Request.Cookies[COOKIES_BASKET] != null)
            {
                cartViewModels = JsonConvert.DeserializeObject<List<CartViewModel>>(Request.Cookies[COOKIES_BASKET]);
            }
            else
            {
                cartViewModels = new List<CartViewModel>();
            }

            return cartViewModels;
        }

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            List<CartViewModel> cartViewModels = GetCartViewModels();
            var itemToRemove = cartViewModels.FirstOrDefault(b => b.ProductId == id);

            if (itemToRemove != null)
            {
                cartViewModels.Remove(itemToRemove);
                UpdateCookie(cartViewModels);
            }

            return Json(new { status = 200 });
        }
    }
}
