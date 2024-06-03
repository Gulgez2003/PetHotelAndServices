using Stripe.Checkout;

namespace Presentation.Controllers
{
    public class CheckoutController : Controller
    {
        public const string COOKIES_BASKET = "cart";
        private readonly ISender _sender;

        public CheckoutController(ISender sender)
        {
            _sender = sender;
        }

        // GET: OrderController/Create
        public async Task<IActionResult> Create()
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
                    Description = product.Description,
                    Title = product.Title,
                    Price = product.Price
                };
                cartItemViewModels.Add(cartItemViewModel);
            }

            return View(cartItemViewModels);
        }


        public IActionResult Index()
        {
            var service = new SessionService();
            Session session = service.Get(TempData["Session"].ToString());
            if (session.PaymentStatus == "paid")
            {
                return RedirectToAction(nameof(OrderConfirmed));
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult OrderConfirmed()
        {
            return View();
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

        public async Task<IActionResult> PlaceOrder()
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
                    Description = product.Description,
                    Title = product.Title,
                    Price = product.Price
                };
                cartItemViewModels.Add(cartItemViewModel);
            }

            var domain = "http://localhost:5077/";
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"Checkout/OrderConfirmed",
                CancelUrl = domain + $"Checkout/Create",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment"
            };

            foreach (var item in cartItemViewModels)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = (long)(item.Price *100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Title
                        }
                    },
                    Quantity = item.ProductCount
                };
                options.LineItems.Add(sessionListItem);
            }

            var service = new SessionService();
            var session = service.Create(options);

            Response.Headers.Add("Location", session.Url);

            return new StatusCodeResult(303);
        }
    }
}
