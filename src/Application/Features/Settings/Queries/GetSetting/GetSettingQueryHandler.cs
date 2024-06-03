namespace FinalProjectApp.Application.Features.Settings.Queries.GetSetting
{
    public class GetSettingQueryHandler : IRequestHandler<GetSettingQuery, SettingGetDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetSettingQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SettingGetDTO> Handle(GetSettingQuery request, CancellationToken cancellationToken)
        {
            var setting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == 4);

            var settingDTO = _mapper.Map<SettingGetDTO>(setting);
            return settingDTO;
        }
    }
}
