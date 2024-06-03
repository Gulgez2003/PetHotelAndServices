namespace FinalProjectApp.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ISender _sender;
        private readonly IApplicationDbContext _context;
        public CategoriesController(ISender sender, IApplicationDbContext context)
        {
            _sender = sender;
            _context = context;
        }

        // GET: CategoriesController
        public async Task<IActionResult> Index()
        {
            var categories = await _sender.Send(new GetCategoriesQuery());
            return View(categories);
        }

        // GET: CategoriesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await _sender.Send(new GetCategoryByIdQuery { Id = id });
            return View(category);
        }

        // GET: CategoriesController/Create
        public async Task<IActionResult> Create(CreateCategoryCommand createCommand)
        {
            var animals = await _sender.Send(new GetAnimalsQuery());
            var subCategories = await _sender.Send(new GetSubCategoriesQuery());

            CategoryAnimalSubCategoryVM categoryAnimalSubCategoryVM = new CategoryAnimalSubCategoryVM()
            {
                CreateCommand = createCommand,
                Animals = animals,
                SubCategories = subCategories
            };

            return View(categoryAnimalSubCategoryVM);
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryAnimalSubCategoryVM categoryAnimalSubCategoryVM)
        {
            CreateCategoryCommandValidator validations = new CreateCategoryCommandValidator(_context);
            ValidationResult validationResult = await validations.ValidateAsync(categoryAnimalSubCategoryVM.CreateCommand);
            if (validationResult.IsValid)
            {
                await _sender.Send(categoryAnimalSubCategoryVM.CreateCommand);
                return RedirectToAction("Index");
            }
            else
            {
                var animals = await _sender.Send(new GetAnimalsQuery());
                var subCategories = await _sender.Send(new GetSubCategoriesQuery());
                categoryAnimalSubCategoryVM.Animals = animals;
                categoryAnimalSubCategoryVM.SubCategories = subCategories;
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
            }
            return View(categoryAnimalSubCategoryVM);
        }

        // GET: CategoriesController/Edit/5
        public async Task<IActionResult> Update(int id)
        {
            var category = await _sender.Send(new GetCategoryByIdQuery { Id = id });
            var animals = await _sender.Send(new GetAnimalsQuery());
            var subCategories = await _sender.Send(new GetSubCategoriesQuery());
            CategoryAnimalSubCategoryVM categoryAnimalSubCategoryVM = new CategoryAnimalSubCategoryVM()
            {
                UpdateCommand = new UpdateCategoryCommand
                {
                    Id = category.Id,
                    Name = category.Name,
                    Animals = category.Animals,
                    SubCategories = category.SubCategories,
                },
                Animals = animals,
                SubCategories = subCategories
            };

            return View(categoryAnimalSubCategoryVM);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CategoryAnimalSubCategoryVM categoryAnimalSubCategoryVM)
        {
            UpdateCategoryCommandValidator validations = new UpdateCategoryCommandValidator(_context);
            ValidationResult validationResult = await validations.ValidateAsync(categoryAnimalSubCategoryVM.UpdateCommand);
            if (validationResult.IsValid)
            {
                await _sender.Send(categoryAnimalSubCategoryVM.UpdateCommand);
                return RedirectToAction("Index");
            }
            else
            {
                var animals = await _sender.Send(new GetAnimalsQuery());
                var subCategories = await _sender.Send(new GetSubCategoriesQuery());
                categoryAnimalSubCategoryVM.Animals = animals;
                categoryAnimalSubCategoryVM.SubCategories = subCategories;
                var category = await _sender.Send(new GetCategoryByIdQuery { Id = categoryAnimalSubCategoryVM.UpdateCommand.Id });
                categoryAnimalSubCategoryVM.UpdateCommand.Name = category.Name;
                categoryAnimalSubCategoryVM.UpdateCommand.Animals = category.Animals;
                categoryAnimalSubCategoryVM.UpdateCommand.SubCategories = category.SubCategories;

                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
            }
            return View(categoryAnimalSubCategoryVM);
        }

        // GET: CategoriesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _sender.Send(new GetCategoryByIdQuery { Id = id });
            if (category == null)
            {
                return NotFound();
            }
            _sender.Send(new DeleteCategoryCommand { Id = id });
            return RedirectToAction("Index");
        }
    }
}