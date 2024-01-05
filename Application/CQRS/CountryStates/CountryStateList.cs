using Application.Core;
using Application.DTOs.CountryStateDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.CountryStates
{
    public class CountryStateList
    {
        public class Query : IRequest<Result<List<CountryStateGetDTO>>> { }

        public class Handler : IRequestHandler<Query, Result<List<CountryStateGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<CountryStateGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var stateList = await _context.CountryStatesDb
                    .Select(m => new CountryStateGetDTO
                    {
                        Id = m.Id,
                        StateName=m.StateName,
                    })
                    .ToListAsync(cancellationToken);

                if (stateList == null)
                {
                    return Result<List<CountryStateGetDTO>>.Failure("no results.");
                }

                return Result<List<CountryStateGetDTO>>.Success(stateList);
            }
        }
    }
}
