using FinalProjectApp.Application.Features.Contacts.Commands.CreateContact;

namespace FinalProjectApp.Presentation.ViewModels
{
    public class SettingContactViewModel
    {
        public CreateContactCommand CreateContactCommand { get; set; }
        public SettingGetDTO SettingGetDTO { get; set; }
    }
}
