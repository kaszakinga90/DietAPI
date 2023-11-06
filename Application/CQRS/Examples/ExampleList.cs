using Application.Core;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB;
using System.Collections.Generic;

namespace Application.CQRS.Examples
{
    public class ExampleList
    {
        public class Query : IRequest<PatientUpdateDTO<List<Example>>> { }

        public class Handler : IRequestHandler<Query, PatientUpdateDTO<List<Example>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<PatientUpdateDTO<List<Example>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return PatientUpdateDTO<List<Example>>.Success(await _context.ExamplesDb.ToListAsync(cancellationToken));
            }
        }
    }
}