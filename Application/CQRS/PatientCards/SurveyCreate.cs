using Application.Core;
using Application.DTOs.PatientsCardsSurveys;
using Application.DTOs.Surveys;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB;
using ModelsDB.Functionality;

namespace Application.CQRS.PatientCards
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

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<SurveyPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var survey = _mapper.Map<Survey>(request.surveyPostDTO);

                _context.SurveysDb.Add(survey);
                await _context.SaveChangesAsync(cancellationToken);

                var surveyPatientCard = new PatientCardSurvey
                {
                    PatientCardId = request.surveyPostDTO.PatientCardId,
                    SurveyId = survey.Id, 
                    DieticianId = survey.DieticianId
                };

                _context.PatientCardSurveysDb.Add(surveyPatientCard);
                await _context.SaveChangesAsync(cancellationToken);

                var resultDto = _mapper.Map<SurveyPostDTO>(survey);
                return Result<SurveyPostDTO>.Success(resultDto);
            }
        }
    }
}
