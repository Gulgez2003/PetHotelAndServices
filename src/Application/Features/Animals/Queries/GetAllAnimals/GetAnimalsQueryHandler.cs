namespace FinalProjectApp.Application.Features.Animals.Queries.GetAllAnimals
{
    public class GetAnimalsQueryHandler : IRequestHandler<GetAnimalsQuery, List<AnimalGetDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAnimalsQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<AnimalGetDTO>> Handle(GetAnimalsQuery request, CancellationToken cancellationToken)
        {
            var animals = await _context.Animals
               .Where(a => !a.IsDeleted)
               .Include(a => a.AnimalImage)
               .OrderBy(a => a.Id)
               .ToListAsync();

            var animalDtos = _mapper.Map<List<AnimalGetDTO>>(animals);

            return animalDtos;
        }
    }
}
