using Application.Core;
using Application.DTOs.ReportTemplateDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ReportTemplates
{
    public class ReportTemplatesPreviewList
    {
        public class Query : IRequest<Result<List<ReportTemplatePreviewDTO>>> { }

        public class Handler : IRequestHandler<Query, Result<List<ReportTemplatePreviewDTO>>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<ReportTemplatePreviewDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var reportTemplatePreview = await _context.ReportTemplatePreviewsDb
                    .ToListAsync(cancellationToken);

                if (reportTemplatePreview.Count == 0 || reportTemplatePreview == null)
                {
                    return Result<List<ReportTemplatePreviewDTO>>.Failure("report template previews nie znaleziono.");
                }
                else
                {
                    var reportTemplatesList = _mapper.Map<List<ReportTemplatePreviewDTO>>(reportTemplatePreview);

                    return Result<List<ReportTemplatePreviewDTO>>.Success(reportTemplatesList);
                }
            }
        }
    }
}
