namespace Presentation.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IApplicationDbContext _context;
        public const string COOKIES_FAVOURITE = "favourite";
        public WishlistController(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<WishlistViewModel> WishlistViewModels = GetWishlistViewModels();
            List<WishlistItemViewModel> favouriteItems = new List<WishlistItemViewModel>();
            foreach (var item in WishlistViewModels)
            {
                var product = await _context.Products
                    .Where(p => p.Id == item.ProductId && !p.IsDeleted)
                    .Include(p => p.SubCategory)
                    .FirstOrDefaultAsync();
                WishlistItemViewModel WishlistItemViewModel = new WishlistItemViewModel()
                {
                    Id = item.ProductId,
                    Title = product.Title,
                    InStock = product.InStock,
                    Price = product.Price,
                    ProductImages = product.ProductImages
                };
                favouriteItems.Add(WishlistItemViewModel);
            }

            return View(favouriteItems);
        }

        public async Task<IActionResult> AddToWishlist(int productId)
        {
            if (productId == 0)
            {
                return NotFound();
            }
            var dbProduct = await _context.Products.Where(p => p.Id == productId && !p.IsDeleted).FirstOrDefaultAsync();
            if (dbProduct == null)
            {
                return BadRequest();
            }

            List<WishlistViewModel> WishlistViewModels = GetWishlistViewModels();
            CheckWishlistViewModel(productId, WishlistViewModels);

            UpdateCookie(WishlistViewModels);

            return RedirectToAction("Index", "Products");
        }

        private void UpdateCookie(List<WishlistViewModel> WishlistViewModels)
        {
            string cookiesFavourite = JsonConvert.SerializeObject(WishlistViewModels);
            Response.Cookies.Append(COOKIES_FAVOURITE, cookiesFavourite);
        }

        private void CheckWishlistViewModel(int id, List<WishlistViewModel> WishlistViewModels)
        {
            WishlistViewModel existFavouriteItem = WishlistViewModels.FirstOrDefault(b => b.ProductId == id);
            if (existFavouriteItem != null)
            {
                WishlistViewModels.Add(existFavouriteItem);
            }
            else
            {
                WishlistViewModels.Add(new WishlistViewModel { ProductId = id });
            }
        }

        private List<WishlistViewModel> GetWishlistViewModels()
        {
            List<WishlistViewModel> WishlistViewModels;

            if (Request.Cookies[COOKIES_FAVOURITE] != null)
            {
                WishlistViewModels = JsonConvert.DeserializeObject<List<WishlistViewModel>>(Request.Cookies[COOKIES_FAVOURITE]);
            }
            else
            {
                WishlistViewModels = new List<WishlistViewModel>();
            }

            return WishlistViewModels;
        }

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            List<WishlistViewModel> WishlistViewModels = GetWishlistViewModels();
            var itemToRemove = WishlistViewModels.FirstOrDefault(b => b.ProductId == id);

            if (itemToRemove != null)
            {
                WishlistViewModels.Remove(itemToRemove);
                UpdateCookie(WishlistViewModels);
            }

            return Json(new { status = 200 });
        }
    }
}
