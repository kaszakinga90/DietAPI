using Application.Core;
using Application.DTOs.TestsResultsDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.PatientCards.TestsResults
{
    public class TestResultDelete
    {
        public class Command : IRequest<Result<TestResultDeleteDTO>>
        {
            public int TestResultId { get; set; }
            public TestResultDeleteDTO TestResultDeleteDTO { get; set; }

            public class Handler : IRequestHandler<Command, Result<TestResultDeleteDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<TestResultDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var testResult = await _context.TestResultsDb
                        .SingleOrDefaultAsync(tr => tr.Id == request.TestResultId);

                    if (testResult == null)
                    {
                        return Result<TestResultDeleteDTO>.Failure("Nie znaleziono wyników badań.");
                    }

                    _mapper.Map(request.TestResultDeleteDTO, testResult);

                    if (testResult != null)
                    {
                        testResult.isActive = false;

                        try
                        {
                            var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                            if (!result)
                            {
                                return Result<TestResultDeleteDTO>.Failure("Operacja nie powiodła się.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                            return Result<TestResultDeleteDTO>.Failure("Wystąpił błąd podczas usuwania test results.");
                        }
                    }
                    return Result<TestResultDeleteDTO>.Success(_mapper.Map<TestResultDeleteDTO>(testResult));
                }
            }
        }
    }
}
