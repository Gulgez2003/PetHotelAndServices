namespace FinalProjectApp.Application.Features.Services.Queries.GetServiceById
{
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, ServiceGetDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetServiceByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceGetDTO> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var service = await _context.Services
                .Where(c => c.Id == request.Id && !c.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            var serviceDTO = _mapper.Map<ServiceGetDTO>(service);
            return serviceDTO;
        }
    }
}
