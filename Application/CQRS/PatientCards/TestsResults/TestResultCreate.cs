using Application.Core;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB;
using Application.DTOs.TestsResultsDTO;
using System.Diagnostics;
using Application.Validators.TestResults;

namespace Application.CQRS.PatientCards.TestsResults
{
    public class TestResultCreate
    {
        public class Command : IRequest<Result<TestResultPostDTO>>
        {
            public TestResultPostDTO TestResultPostDTO { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<TestResultPostDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly TestResultCreateValidator _validator;

            public Handler(DietContext context, IMapper mapper, TestResultCreateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<Result<TestResultPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator
                    .ValidateAsync(request.TestResultPostDTO);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    return Result<TestResultPostDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                }

                try
                {
                    var singleTestResults = _mapper.Map<SingleTestResults>(request.TestResultPostDTO);

                    _context.SingleTestResultsDb.Add(singleTestResults);
                    await _context.SaveChangesAsync();

                    var testResult = new TestResult
                    {
                        PatientCardId = request.TestResultPostDTO.PatientCardId,
                        DieticianId = singleTestResults.DieticianId,
                        SingleTestResultsId = singleTestResults.Id,
                    };

                    _context.TestResultsDb.Add(testResult);
                    await _context.SaveChangesAsync();

                    var resultDto = _mapper.Map<TestResultPostDTO>(singleTestResults);
                    return Result<TestResultPostDTO>.Success(resultDto);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<TestResultPostDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}