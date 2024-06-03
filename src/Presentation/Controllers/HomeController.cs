namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISender _sender;

        public HomeController(ISender sender)
        {
            _sender = sender;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _sender.Send(new GetProductsQuery());
            var services = await _sender.Send(new GetServicesQuery());

            ServiceProductViewModel viewModel = new ServiceProductViewModel
            {
                Products = products,
                Services = services
            };

            return View(viewModel);
        }
    }
}
