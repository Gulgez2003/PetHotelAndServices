namespace FinalProjectApp.Application.Features.Services.Queries.GetAllServices
{
    public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, List<ServiceGetDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetServicesQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<ServiceGetDTO>> Handle(GetServicesQuery request, CancellationToken cancellationToken)
        {
            var services = await _context.Services
               .OrderBy(c => c.Id)
               .ToListAsync();

            var serviceDTOs = _mapper.Map<List<ServiceGetDTO>>(services);

            return serviceDTOs;
        }
    }
}
