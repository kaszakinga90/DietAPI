using Application.Core;
using DietDB;
using MediatR;
using ModelsDB;

namespace Application.CQRS.Examples
{
    public class Details
    {
        public class Query : IRequest<PatientUpdateDTO<Example>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, PatientUpdateDTO<Example>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<PatientUpdateDTO<Example>> Handle(Query request, CancellationToken cancellationToken)
            {
                var example = await _context.Examples.FindAsync(request.Id);
                return PatientUpdateDTO<Example>.Success(example);
            }
        }
    }
}
