using Application.Core;
using Application.DTOs.Surveys;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.PatientCards.Surveys
{
    public class SurveyDelete
    {
        public class Command : IRequest<Result<SurveyDeleteDTO>>
        {
            public int SurveyId { get; set; }
            public SurveyDeleteDTO SurveyDeleteDTO { get; set; }

            public class Handler : IRequestHandler<Command, Result<SurveyDeleteDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<SurveyDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var survey = await _context.SurveysDb
                        .Include(s => s.PatientCardSurveys)
                        .SingleOrDefaultAsync(s => s.Id == request.SurveyId);

                    if (survey == null)
                    {
                        return Result<SurveyDeleteDTO>.Failure("Nie znaleziono wywiadu.");
                    }

                    _mapper.Map(request.SurveyDeleteDTO, survey);

                    if (survey != null)
                    {
                        survey.isActive = false;

                        foreach (var patientCardSurvey in survey.PatientCardSurveys)
                        {
                            patientCardSurvey.isActive = false;
                        }

                        try
                        {
                            var result = await _context.SaveChangesAsync() > 0;
                            if (!result)
                            {
                                return Result<SurveyDeleteDTO>.Failure("Operacja nie powiodła się.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                            return Result<SurveyDeleteDTO>.Failure("Wystąpił błąd podczas usuwania test results.");
                        }
                    }
                    return Result<SurveyDeleteDTO>.Success(_mapper.Map<SurveyDeleteDTO>(survey));
                }
            }
        }
    }
}