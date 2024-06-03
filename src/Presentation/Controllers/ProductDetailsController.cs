namespace Presentation.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly ISender _sender;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProductDetailsController(ISender sender, UserManager<ApplicationUser> userManager)
        {
            _sender = sender;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int id)
        {
            var products = await _sender.Send(new GetProductsQuery());
            var product = await _sender.Send(new GetProductByIdQuery { Id = id });
            var testimonials = await _sender.Send(new GetPublishedCommentsByIdQuery());

            ProductsViewModel productsViewModel = new ProductsViewModel
            {
                Product = product,
                Products = products,
                Testimonials = testimonials,
                CreateCommentCommand = new CreateCommentCommand()
            };

            return View(productsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ProductsViewModel productsViewModel)
        {
            CreateCommentCommandValidator validations = new CreateCommentCommandValidator();
            ValidationResult validationResult = await validations.ValidateAsync(productsViewModel.CreateCommentCommand);
            if (validationResult.IsValid)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Json("Please login or register first");
                }

                string username = User.Identity.Name;
                ApplicationUser user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    return Json("Make sure you fill in the fields correctly");
                }

                productsViewModel.CreateCommentCommand.Email = user.Email;
                productsViewModel.CreateCommentCommand.FullName = user.FullName;

                await _sender.Send(productsViewModel.CreateCommentCommand);
                return Json(new { success = true });
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return Json(new { success = false, message = "Some error message" });
            }
        }
    }
}
