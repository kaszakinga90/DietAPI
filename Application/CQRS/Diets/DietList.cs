﻿using Application.Core;
using DietDB;
using MediatR;

namespace Application.CQRS.Diets
{
    public class DietList
    {
        public class Query : IRequest<Result<PagedList<DietGetDTO>>>
        {
            public int DieticianId { get; set; }
            public PagingParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<DietGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PagedList<DietGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dietsList = _context.DietsDb
                    .Where(d => d.DieteticianId == request.DieticianId)
                    .Select(d => new DietGetDTO
                    {
                        Id = d.Id,
                        DieteticianId = d.DieteticianId,
                        Name = d.Name,
                        StartDate = d.StartDate.Date,
                        EndDate = d.EndDate.Date,
                        numberOfMeals = d.numberOfMeals,
                        PatientName = d.Patient.FirstName + " " + d.Patient.LastName,
                        DieteticanName =d.Dietician.FirstName + " " + d.Dietician.LastName,
                    })
                    .AsQueryable();
                return Result<PagedList<DietGetDTO>>.Success(
                    await PagedList<DietGetDTO>.CreateAsync(dietsList, request.Params.PageNumber, request.Params.PageSize)
                    );
            }
        }
    }
}

