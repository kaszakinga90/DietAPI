using Application.Core;
using Application.DTOs.DiplomaDTO;
using Application.DTOs.SpecializationDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Specializations
{
    public class SpecializationsList
    {
        public class Query : IRequest<Result<List<SpecializationGetDTO>>>
        {
            public class Handler : IRequestHandler<Query, Result<List<SpecializationGetDTO>>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<List<SpecializationGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
                {
                    var specializationsList = await _context.SpecializationsDb
                        .Select(m => new SpecializationGetDTO
                        {
                            Id = m.Id,
                            SpecializationName=m.SpecializationName
                        })
                        .ToListAsync();
                    return Result<List<SpecializationGetDTO>>.Success(specializationsList);
                }
            }
        }
    }
}