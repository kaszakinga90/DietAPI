using Application.Core;
using Application.DTOs.ReportTemplateDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ReportTemplates
{
    public class ReportTemplateDetails
    {
        public class Query : IRequest<Result<ReportTemplateGetDTO>>
        {
            public int TemplateId { get; set; }
            public class Handler : IRequestHandler<Query, Result<ReportTemplateGetDTO>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<ReportTemplateGetDTO>> Handle(Query request, CancellationToken cancellationToken)
                {
                    var reportTemplate = await _context.ReportTemplatesDb
                        .Where(m => m.Id == request.TemplateId)
                        .Select(m => new ReportTemplateGetDTO
                        {
                            Id = m.Id,
                            Name = m.Name
                        })
                        .FirstOrDefaultAsync();

                    if (reportTemplate == null)
                    {
                        return Result<ReportTemplateGetDTO>.Failure("report template not found.");
                    }

                    return Result<ReportTemplateGetDTO>.Success(reportTemplate);
                }
            }
        }
    }
}
