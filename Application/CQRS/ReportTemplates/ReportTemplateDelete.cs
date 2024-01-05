using Application.Core;
using Application.DTOs.ReportTemplateDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ReportTemplates
{
    public class ReportTemplateDelete
    {
        public class Command : IRequest<Result<ReportTemplateDeleteDTO>>
        {
            public int Id { get; set; }
            public ReportTemplateDeleteDTO ReportTemplateDeleteDTO { get; set; }

        }
        public class Handler : IRequestHandler<Command, Result<ReportTemplateDeleteDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<ReportTemplateDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var reportTemplate = await _context.ReportTemplatesDb.FirstOrDefaultAsync(i => i.Id == request.Id);

                if (reportTemplate == null)
                {
                    return Result<ReportTemplateDeleteDTO>.Failure("report template o podanym ID nie została znaleziona.");
                }

                _mapper.Map(request.ReportTemplateDeleteDTO, reportTemplate);

                reportTemplate.isActive = false;

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<ReportTemplateDeleteDTO>.Failure("Usunięcie report template nie powiodło się.");
                    }
                }
                catch (Exception ex)
                {
                    return Result<ReportTemplateDeleteDTO>.Failure("Wystąpił błąd podczas usuwania report template.");
                }

                return Result<ReportTemplateDeleteDTO>.Success(_mapper.Map<ReportTemplateDeleteDTO>(reportTemplate));
            }
        }
    }
}
