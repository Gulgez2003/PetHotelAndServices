
namespace FinalProjectApp.Application.Features.Settings.Queries.GetAllSettings
{
    public class SettingGetDTO
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Information { get; set; }
        public string TwitterIcon { get; set; }
        public string FaceBookIcon { get; set; }
        public string InstagramIcon { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public static implicit operator List<object>(SettingGetDTO v)
        {
            throw new NotImplementedException();
        }
    }
}
