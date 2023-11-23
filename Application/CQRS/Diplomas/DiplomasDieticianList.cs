using Application.Core;
using Application.DTOs.DiplomaDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Diplomas
{
    public class DiplomasDieticianList
    {
        public class Query : IRequest<Result<List<DiplomaGetDTO>>>
        {
            public int DieticianId { get; set; }

            public class Handler : IRequestHandler<Query, Result<List<DiplomaGetDTO>>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<List<DiplomaGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
                {
                    var diplomaList = await _context.DiplomasDb
                        .Where(m => m.DieticianId == request.DieticianId)
                        .Select(m => new DiplomaGetDTO
                        {
                            Id = m.Id,
                            Title = m.Title,
                            Description = m.Description,
                            PictureUrl = m.PictureUrl,
                        })
                        .ToListAsync();
                    return Result<List<DiplomaGetDTO>>.Success(diplomaList);
                }
            }
        }
    }
}

