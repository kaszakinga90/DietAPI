using Application.Core;
using Application.DTOs.ReportTemplateDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ReportTemplates
{
    public class ReportTemplatesList
    {
        public class Query : IRequest<Result<List<ReportTemplateGetDTO>>> { }

        public class Handler : IRequestHandler<Query, Result<List<ReportTemplateGetDTO>>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<ReportTemplateGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var reportTemplateFromDb = await _context.ReportTemplatesDb
                    .ToListAsync(cancellationToken);

                if (reportTemplateFromDb.Count == 0 || reportTemplateFromDb == null) {
                    return Result<List<ReportTemplateGetDTO>>.Failure("Nie znaleziono szablonu raportu.");
                } 
                else
                {
                    var reportTemplatesList = _mapper.Map<List<ReportTemplateGetDTO>>(reportTemplateFromDb);

                    return Result<List<ReportTemplateGetDTO>>.Success(reportTemplatesList);
                }
            }
        }
    }
}