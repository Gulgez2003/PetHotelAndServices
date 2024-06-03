using FinalProjectApp.Application.Features.Settings.Queries.GetSetting;

namespace FinalProjectApp.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingsController : Controller
    {
        private readonly ISender _sender;

        public SettingsController(ISender sender)
        {
            _sender = sender;
        }
        // GET: SettingsController
        public async Task<IActionResult> Index()
        {
            var setting = await _sender.Send(new GetSettingQuery());
            return View(setting);
        }

        // GET: SettingsController/Edit
        public async Task<IActionResult> Update()
        {
            var setting = await _sender.Send(new GetSettingQuery ());
            UpdateSettingCommand updateSettingCommand = new UpdateSettingCommand()
            {
                Address = setting.Address,
                PhoneNumber = setting.PhoneNumber,
                Email = setting.Email,
                Information = setting.Information,
                InstagramIcon = setting.InstagramIcon,
                FaceBookIcon = setting.FaceBookIcon,
                TwitterIcon = setting.TwitterIcon
            };

            return View(updateSettingCommand);
        }

        // POST: SettingsController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateSettingCommand command)
        {
            UpdateSettingCommandValidator validations = new UpdateSettingCommandValidator();
            ValidationResult validationResult = await validations.ValidateAsync(command);
            if (validationResult.IsValid)
            {
                await _sender.Send(command);
                return RedirectToAction("Index");
            }
            else
            {
                var setting = await _sender.Send(new GetSettingQuery ());
                command.Information = setting.Information;
                command.PhoneNumber = setting.PhoneNumber;
                command.Email = setting.Email;
                command.Address = setting.Address;
                command.FaceBookIcon = setting.FaceBookIcon;
                command.TwitterIcon = setting.TwitterIcon;
                command.InstagramIcon = setting.InstagramIcon;

                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
            }
            return View(command);
        }
    }
}
