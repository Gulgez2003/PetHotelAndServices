namespace FinalProjectApp.Application.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly IApplicationDbContext _context;

        public LayoutService(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public SettingGetDTO GetSetting()
        {
            Setting setting = _context.Settings.FirstOrDefault(s => s.Id == 4);
            SettingGetDTO settingGetDTO = new SettingGetDTO()
            {
                Information = setting.Information,
                PhoneNumber = setting.PhoneNumber,
                Email = setting.Email,
                Address = setting.Address,
                FaceBookIcon = setting.FaceBookIcon,
                TwitterIcon = setting.TwitterIcon,
                InstagramIcon = setting.InstagramIcon
            };
            return settingGetDTO;
        }
    }
}
