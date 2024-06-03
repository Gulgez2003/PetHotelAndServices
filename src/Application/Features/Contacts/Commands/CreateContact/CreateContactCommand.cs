namespace FinalProjectApp.Application.Features.Contacts.Commands.CreateContact
{
    public class CreateContactCommand : IRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
