using Application.Core;
using Application.DTOs.PatientCardDTO;
using Application.Validators.PatientCard;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

// TODO : obsługa - dokończ. funkcjonalności (czy należy zmapować? )
// TODO : tu musi być inna obsługa handle żeby walidacja działała
namespace Application.CQRS.PatientCards
{
    public class PatientCardCreate
    {
        public class Command : IRequest
        {
            public PatientCardPostDTO PatientCardPostDTO { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly PatientCardCreateValidator _validator;

            public Handler(DietContext context, IMapper mapper, PatientCardCreateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                //var validationResult = await _validator
                //    .ValidateAsync(request.PatientCardPostDTO, cancellationToken);

                //if (!validationResult.IsValid)
                //{
                //    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                //    return Result<PatientCardPostDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                //}

                try
                {
                    var pc = request.PatientCardPostDTO;
                    var parameters = new[]
                    {
                         new SqlParameter("@PatientId", pc.PatientId),
                        new SqlParameter("@DieticianId", pc.DieticianId),
                        new SqlParameter("@SexId", pc.SexId)
                    };

                    await _context.Database.ExecuteSqlRawAsync("EXEC CreatePatientCard @PatientId, @DieticianId, @SexId", parameters);

                    //var createdPatientCardId = await _context.PatientCardsDb
                    //    .Where(pc => pc.PatientId == request.PatientId && pc.DieticianId == request.DieticianId)
                    //    .Select(pc => pc.Id)
                    //    .FirstOrDefaultAsync();

                    //return createdPatientCardId;
                }
                catch
                {
                    throw; // TODO:  obsługa błędów
                }
            }

        }
    }
}
