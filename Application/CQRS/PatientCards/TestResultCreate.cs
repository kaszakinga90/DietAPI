using Application.Core;
using Application.DTOs.Surveys;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;
using ModelsDB;
using Application.DTOs.TestsResultsDTO;

namespace Application.CQRS.PatientCards
{
    public class TestResultCreate
    {
        public class Command : IRequest<Result<TestResultPostDTO>>
        {
            public TestResultPostDTO testResultPost { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<TestResultPostDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<TestResultPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var singleTestResults = _mapper.Map<SingleTestResults>(request.testResultPost);

                _context.SingleTestResultsDb.Add(singleTestResults);
                await _context.SaveChangesAsync(cancellationToken);

                var testResult = new TestResult
                {
                    PatientCardId = request.testResultPost.PatientCardId,
                    DieticianId = singleTestResults.DieticianId,
                    SingleTestResultsId=singleTestResults.Id,
                };

                _context.TestResultsDb.Add(testResult);
                await _context.SaveChangesAsync(cancellationToken);

                var resultDto = _mapper.Map<TestResultPostDTO>(singleTestResults);
                return Result<TestResultPostDTO>.Success(resultDto);
            }
        }
    }
}