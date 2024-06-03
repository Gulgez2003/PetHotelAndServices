namespace FinalProjectApp.Application.Features.Settings.Commands.UpdateSetting
{
    public class UpdateSettingCommandHandler : IRequestHandler<UpdateSettingCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateSettingCommandHandler(IApplicationDbContext context) => _context = context;
        public async Task Handle(UpdateSettingCommand request, CancellationToken cancellationToken)
        {
            var setting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == 4);

            setting.Address = request.Address;
            setting.PhoneNumber = request.PhoneNumber;
            setting.Email = request.Email;
            setting.Information = request.Information;
            setting.TwitterIcon = request.TwitterIcon;
            setting.FaceBookIcon = request.FaceBookIcon;
            setting.InstagramIcon = request.InstagramIcon;
            setting.LastModifiedBy = "Admin";
            setting.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
