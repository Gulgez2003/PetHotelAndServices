namespace FinalProjectApp.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ISender _sender;
        private readonly IApplicationDbContext _context;
        public ProductsController(ISender sender, IApplicationDbContext context)
        {
            _sender = sender;
            _context = context;
        }

        // GET: ProductsController
        public async Task<IActionResult> Index()
        {
            var products = await _sender.Send(new GetProductsQuery());
            return View(products);
        }

        // GET: ProductsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _sender.Send(new GetProductByIdQuery { Id = id });
            return View(product);
        }

        // GET: ProductsController/Create
        public async Task<IActionResult> Create(CreateProductCommand createCommand)
        {
            var subCategories = await _sender.Send(new GetSubCategoriesQuery());
            ProductSubCategoryVM productSubCategoryVM = new ProductSubCategoryVM()
            {
                CreateCommand = createCommand,
                SubCategories = subCategories,
                Colours = Enum.GetValues(typeof(Colour)).OfType<Colour>()
            };
            return View(productSubCategoryVM);
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductSubCategoryVM productSubCategoryVM)
        {
            CreateProductCommandValidator validations = new CreateProductCommandValidator(_context);
            ValidationResult validationResult = await validations.ValidateAsync(productSubCategoryVM.CreateCommand);
            if (validationResult.IsValid)
            {
                await _sender.Send(productSubCategoryVM.CreateCommand);
                return RedirectToAction("Index");
            }
            else
            {
                var subCategories = await _sender.Send(new GetSubCategoriesQuery());
                productSubCategoryVM.SubCategories = subCategories;
                productSubCategoryVM.Colours = Enum.GetValues(typeof(Colour)).OfType<Colour>();

                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
            }
            return View(productSubCategoryVM);
        }

        // GET: ProductsController/Edit/5
        public async Task<IActionResult> Update(int id)
        {
            var product = await _sender.Send(new GetProductByIdQuery { Id = id });
            var subCategories = await _sender.Send(new GetSubCategoriesQuery());
            ProductSubCategoryVM productSubCategoryVM = new ProductSubCategoryVM()
            {
                UpdateCommand = new UpdateProductCommand
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    UnitsInStock = product.UnitsInStock,
                    Price = product.Price,
                    SubCategoryId = product.SubCategory.Id,
                    Colours = product.Colours,
                    ProductImages = product.ProductImages
                },
                SubCategories = subCategories,
                Colours = Enum.GetValues(typeof(Colour)).OfType<Colour>()
            };

            return View(productSubCategoryVM);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProductSubCategoryVM productSubCategoryVM)
        {
            UpdateProductCommandValidator validations = new UpdateProductCommandValidator(_context);
            ValidationResult validationResult = await validations.ValidateAsync(productSubCategoryVM.UpdateCommand);
            if (validationResult.IsValid)
            {
                await _sender.Send(productSubCategoryVM.UpdateCommand);
                return RedirectToAction("Index");
            }
            else
            {
                var subCategories = await _sender.Send(new GetSubCategoriesQuery());
                productSubCategoryVM.SubCategories = subCategories;
                productSubCategoryVM.Colours = Enum.GetValues(typeof(Colour)).OfType<Colour>();
                var product = await _sender.Send(new GetProductByIdQuery { Id = productSubCategoryVM.UpdateCommand.Id });
                productSubCategoryVM.UpdateCommand.Title = product.Title;
                productSubCategoryVM.UpdateCommand.Description = product.Description;
                productSubCategoryVM.UpdateCommand.UnitsInStock = product.UnitsInStock;
                productSubCategoryVM.UpdateCommand.Price = product.Price;
                productSubCategoryVM.UpdateCommand.SubCategoryId = product.SubCategory.Id;
                productSubCategoryVM.UpdateCommand.Colours = product.Colours;
                productSubCategoryVM.UpdateCommand.ProductImages = product.ProductImages;

                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
            }
            return View(productSubCategoryVM);
        }

        // GET: ProductsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _sender.Send(new GetProductByIdQuery { Id = id });
            if (product == null)
            {
                return NotFound();
            }
            _sender.Send(new DeleteProductCommand { Id = id });
            return RedirectToAction("Index");
        }
    }
}
