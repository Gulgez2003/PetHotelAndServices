namespace FinalProjectApp.Application.Features.Animals.Queries.GetAnimalById
{
    public class GetAnimalByIdQueryHandler : IRequestHandler<GetAnimalByIdQuery, AnimalGetDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAnimalByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AnimalGetDTO> Handle(GetAnimalByIdQuery request, CancellationToken cancellationToken)
        {
            var animal = await _context.Animals
                .Where(a => a.Id == request.Id && !a.IsDeleted)
                .Include(a => a.Categories)
                .Include(a => a.AnimalImage)
                .FirstOrDefaultAsync(cancellationToken);

            var animalDTO = _mapper.Map<AnimalGetDTO>(animal);
            return animalDTO;
        }
    }
}
