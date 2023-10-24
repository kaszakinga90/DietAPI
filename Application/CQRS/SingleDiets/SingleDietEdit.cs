using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;

namespace Application.CQRS.SingleDiets
{
    public class SingleDietEdit
    {
        public class Command : IRequest
        {
            public SingleDiet SingleDiet { get; set; }
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
                var SingleDiet = await _context.SingleDiet.FindAsync(request.SingleDiet.Id);
                _mapper.Map(request.SingleDiet, SingleDiet);
                await _context.SaveChangesAsync();
            }
        }
    }
}
