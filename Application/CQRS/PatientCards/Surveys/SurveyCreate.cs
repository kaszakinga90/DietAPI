using Application.Core;
using Application.DTOs.Surveys;
using Application.Validators.Survey;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB;
using ModelsDB.Functionality;
using System.Diagnostics;

namespace Application.CQRS.PatientCards.Surveys
{
    public class SurveyCreate
    {
        public class Command : IRequest<Result<SurveyPostDTO>>
        {
            public SurveyPostDTO surveyPostDTO { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<SurveyPostDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly SurveyCreateValidator _validator;

            public Handler(DietContext context, IMapper mapper, SurveyCreateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<Result<SurveyPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator
                    .ValidateAsync(request.surveyPostDTO);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    return Result<SurveyPostDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                }

                try
                {
                    var survey = _mapper.Map<Survey>(request.surveyPostDTO);

                    _context.SurveysDb.Add(survey);
                    await _context.SaveChangesAsync();

                    var surveyPatientCard = new PatientCardSurvey
                    {
                        PatientCardId = request.surveyPostDTO.PatientCardId,
                        SurveyId = survey.Id,
                        DieticianId = survey.DieticianId
                    };

                    _context.PatientCardSurveysDb.Add(surveyPatientCard);
                    await _context.SaveChangesAsync();

                    var resultDto = _mapper.Map<SurveyPostDTO>(survey);

                    return Result<SurveyPostDTO>.Success(resultDto);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<SurveyPostDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}