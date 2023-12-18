using Application.Core;
using Application.DTOs.DieticianOfficeDTO;
using Application.DTOs.FoodCatalogDTO;
using Application.DTOs.OfficeDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Offices
{
    public class OfficeList
    {
        public class Query : IRequest<Result<List<DieticianOfficesGetDTO>>>
        {
            public int DieteticianId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<DieticianOfficesGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<DieticianOfficesGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var offices = await _context.DieticianOffices
                    .Where(m => m.DieticianId == request.DieteticianId)
                    .Select(m => new DieticianOfficesGetDTO
                    {
                        OfficeId=m.OfficeId,
                        OfficeName=m.Office.OfficeName,
                    })
                    .ToListAsync(cancellationToken);
                return Result<List<DieticianOfficesGetDTO>>.Success(offices);
            }
        }
    }
}
