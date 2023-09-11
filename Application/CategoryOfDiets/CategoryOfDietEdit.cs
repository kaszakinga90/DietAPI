using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;

namespace Application.CategoryOfDiets
{
    public class CategoryOfDietEdit
    {
        public class Command : IRequest
        {
            public CategoryOfDiet CategoryOfDiet { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var CategoryOfDiet = await _context.CategoryOfDiet.FindAsync(request.CategoryOfDiet.Id);
                _mapper.Map(request.CategoryOfDiet, CategoryOfDiet);
                await _context.SaveChangesAsync();
            }
        }
    }
}
