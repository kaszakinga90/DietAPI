using Application.DTOs.PatientCardDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.PatientCards
{
    public class PatientCardCreate
    {
        public class Command : IRequest
        {
            public PatientCardPostDTO PatientCardPostDTO { get; set; }
            public int PatientId { get; set; }
            public int DieticianId { get; set; }
            public int SexId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {

                try
                {
                    var parameters = new[]
                    {
                        new SqlParameter("@PatientId", request.PatientId),
                        new SqlParameter("@DieticianId", request.DieticianId),
                        new SqlParameter("@SexId", request.SexId)
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
