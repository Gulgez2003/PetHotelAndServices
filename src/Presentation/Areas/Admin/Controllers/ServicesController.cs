using FinalProjectApp.Application.Features.Services.Commands.DeleteCommand;

namespace FinalProjectApp.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServicesController : Controller
    {
        private readonly ISender _sender;
        private readonly IApplicationDbContext _context;

        public ServicesController(IApplicationDbContext context, ISender sender)
        {
            _context = context;
            _sender = sender;
        }

        // GET: ServicesController
        public async Task<IActionResult> Index()
        {
            var services = await _sender.Send(new GetServicesQuery());
            return View(services);
        }

        // GET: ServicesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var service = await _sender.Send(new GetServiceByIdQuery { Id = id });
            return View(service);
        }

        // GET: ServicesController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ServicesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateServiceCommand createCommand)
        {
            CreateServiceCommandValidator validations = new CreateServiceCommandValidator(_context);
            ValidationResult validationResult = await validations.ValidateAsync(createCommand);
            if (validationResult.IsValid)
            {
                await _sender.Send(createCommand);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
            }
            return View(createCommand);
        }

        // GET: ServicesController/Edit/5
        public async Task<IActionResult> Update(int id)
        {
            var service = await _sender.Send(new GetServiceByIdQuery { Id = id });
            UpdateServiceCommand updateCommand = new UpdateServiceCommand
            {
                Id = service.Id,
                Title = service.Title,
                Description = service.Description
            };

            return View(updateCommand);
        }

        // POST: ServicesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateServiceCommand updateCommand)
        {
            UpdateServiceCommandValidator validations = new UpdateServiceCommandValidator(_context);
            ValidationResult validationResult = await validations.ValidateAsync(updateCommand);
            if (validationResult.IsValid)
            {
                await _sender.Send(updateCommand);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
            }
            return View(updateCommand);
        }

        // GET: ServicesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _sender.Send(new GetServiceByIdQuery { Id = id });
            if (service == null)
            {
                return NotFound();
            }
            _sender.Send(new DeleteServiceCommand { Id = id });
            return RedirectToAction("Index");
        }
    }
}
