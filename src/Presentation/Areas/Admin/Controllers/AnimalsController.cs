namespace FinalProjectApp.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AnimalsController : Controller
    {
        private readonly ISender _sender;
        private readonly IApplicationDbContext _context;
        public AnimalsController(ISender sender, IApplicationDbContext context)
        {
            _sender = sender;
            _context = context;
        }

        // GET: AnimalsController
        public async Task<IActionResult> Index()
        {
            var animals = await _sender.Send(new GetAnimalsQuery());
            return View(animals);
        }

        // GET: AnimalsController/Details/5
            public async Task<IActionResult> Details(int id)
            {
                var animal = await _sender.Send(new GetAnimalByIdQuery { Id = id });
                return View(animal);
            }

        // GET: AnimalsController/Create
        public async Task<IActionResult> Create(CreateAnimalCommand createCommand)
        {
            var categories = await _sender.Send(new GetCategoriesQuery());
            AnimalCategoryVM animalCategoryVM = new AnimalCategoryVM()
            {
                CreateCommand = createCommand,
                Categories = categories
            };
            return View(animalCategoryVM);
        }

        // POST: AnimalsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimalCategoryVM animalCategoryVM)
        {
            CreateAnimalCommandValidator validations = new CreateAnimalCommandValidator(_context);
            ValidationResult validationResult = await validations.ValidateAsync(animalCategoryVM.CreateCommand);
            if (validationResult.IsValid)
            {
                await _sender.Send(animalCategoryVM.CreateCommand);
                return RedirectToAction("Index");
            }
            else
            {
                var categories = await _sender.Send(new GetCategoriesQuery());
                animalCategoryVM.Categories = categories;
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
            }
            return View(animalCategoryVM);
        }

        // GET: AnimalsController/Edit/5
        public async Task<IActionResult> Update(int id)
        {
            var animal = await _sender.Send(new GetAnimalByIdQuery { Id = id });
            var categories = await _sender.Send(new GetCategoriesQuery());
            AnimalCategoryVM animalCategoryVM = new AnimalCategoryVM()
            {
                UpdateCommand = new UpdateAnimalCommand
                {
                    Id = animal.Id,
                    Name = animal.Name,
                    AnimalImage = animal.AnimalImage,
                    Categories = animal.Categories
                },
                Categories = categories
            };

            return View(animalCategoryVM);
        }

        // POST: AnimalsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AnimalCategoryVM animalCategoryVM)
        {
            UpdateAnimalCommandValidator validations = new UpdateAnimalCommandValidator(_context);
            ValidationResult validationResult = await validations.ValidateAsync(animalCategoryVM.UpdateCommand);
            if (validationResult.IsValid)
            {
                await _sender.Send(animalCategoryVM.UpdateCommand);
                return RedirectToAction("Index");
            }
            else
            {
                var categories = await _sender.Send(new GetCategoriesQuery());
                animalCategoryVM.Categories = categories;
                var animal = await _sender.Send(new GetAnimalByIdQuery { Id = animalCategoryVM.UpdateCommand.Id });
                animalCategoryVM.UpdateCommand.Name = animal.Name;
                animalCategoryVM.UpdateCommand.Categories = animal.Categories;
                animalCategoryVM.UpdateCommand.AnimalImage = animal.AnimalImage;

                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
            }
            return View(animalCategoryVM);
        }

        [HttpPost]
        // GET: AnimalsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var animal = await _sender.Send(new GetAnimalByIdQuery { Id = id });
            if (animal == null)
            {
                return NotFound();
            }

            await _sender.Send(new DeleteAnimalCommand { Id = id });
            return RedirectToAction("Index");
        }
    }
}
