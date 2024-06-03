namespace FinalProjectApp.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoriesController : Controller
    {
        private readonly ISender _sender;
        private readonly IApplicationDbContext _context;
        public SubCategoriesController(ISender sender, IApplicationDbContext context)
        {
            _sender = sender;
            _context = context;
        }
        // GET: SubCategoriesController
        public async Task<IActionResult> Index()
        {
            var subCategories = await _sender.Send(new GetSubCategoriesQuery());
            return View(subCategories);
        }

        // GET: SubCategoriesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var subCategory = await _sender.Send(new GetSubCategoryByIdQuery { Id = id });
            return View(subCategory);
        }

        // GET: SubCategoriesController/Create
        public async Task<IActionResult> Create(CreateSubCategoryCommand createCommand)
        {
            var products = await _sender.Send(new GetProductsQuery());
            var categories = await _sender.Send(new GetCategoriesQuery());

            SubCategoryProductCategoryVM subCategoryProductCategoryVM = new SubCategoryProductCategoryVM()
            {
                CreateCommand = createCommand,
                Products = products,
                Categories = categories
            };

            return View(subCategoryProductCategoryVM);
        }

        // POST: SubCategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategoryProductCategoryVM subCategoryProductCategoryVM)
        {
            CreateSubCategoryCommandValidator validations = new CreateSubCategoryCommandValidator(_context);
            ValidationResult validationResult = await validations.ValidateAsync(subCategoryProductCategoryVM.CreateCommand);
            if (validationResult.IsValid)
            {
                await _sender.Send(subCategoryProductCategoryVM.CreateCommand);
                return RedirectToAction("Index");
            }
            else
            {
                var products = await _sender.Send(new GetProductsQuery());
                var categories = await _sender.Send(new GetCategoriesQuery());
                subCategoryProductCategoryVM.Products = products;
                subCategoryProductCategoryVM.Categories = categories;

                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
            }
            return View(subCategoryProductCategoryVM);
        }

        // GET: SubCategoriesController/Edit/5
        public async Task<IActionResult> Update(int id)
        {
            var subCategory = await _sender.Send(new GetSubCategoryByIdQuery { Id = id });
            var products = await _sender.Send(new GetProductsQuery());
            var categories = await _sender.Send(new GetCategoriesQuery());
            SubCategoryProductCategoryVM subCategoryProductCategoryVM = new SubCategoryProductCategoryVM()
            {
                UpdateCommand = new UpdateSubCategoryCommand
                {
                    Id = subCategory.Id,
                    Name = subCategory.Name,
                    CategoryId = subCategory.Category.Id,
                    Products = subCategory.Products
                },
                Products = products,
                Categories = categories
            };

            return View(subCategoryProductCategoryVM);
        }

        // POST: SubCategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SubCategoryProductCategoryVM subCategoryProductCategoryVM)
        {
            UpdateSubCategoryCommandValidator validations = new UpdateSubCategoryCommandValidator(_context);
            ValidationResult validationResult = await validations.ValidateAsync(subCategoryProductCategoryVM.UpdateCommand);
            if (validationResult.IsValid)
            {
                await _sender.Send(subCategoryProductCategoryVM.UpdateCommand);
                return RedirectToAction("Index");
            }
            else
            {
                var products = await _sender.Send(new GetProductsQuery());
                var categories = await _sender.Send(new GetCategoriesQuery());
                subCategoryProductCategoryVM.Products = products;
                subCategoryProductCategoryVM.Categories = categories;
                var subCategory = await _sender.Send(new GetSubCategoryByIdQuery { Id = subCategoryProductCategoryVM.UpdateCommand.Id });
                subCategoryProductCategoryVM.UpdateCommand.Name = subCategory.Name;
                subCategoryProductCategoryVM.UpdateCommand.CategoryId = subCategory.Category.Id;
                subCategoryProductCategoryVM.UpdateCommand.Products = subCategory.Products;

                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
            }
            return View(subCategoryProductCategoryVM);
        }

        // GET: SubCategoriesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var subCategory = await _sender.Send(new GetSubCategoryByIdQuery { Id = id });
            if (subCategory == null)
            {
                return NotFound();
            }
            _sender.Send(new DeleteSubCategoryCommand { Id = id });
            return RedirectToAction("Index");
        }
    }
}
