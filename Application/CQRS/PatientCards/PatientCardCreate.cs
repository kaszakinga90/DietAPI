using Application.Core;
using Application.DTOs.PatientCardDTO;
using Application.Validators.PatientCard;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.PatientCards
{
    public class PatientCardCreate
    {
        public class Command : IRequest<Result<PatientCardPostDTO>>
        {
            public PatientCardPostDTO PatientCardPostDTO { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<PatientCardPostDTO>>
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

            public async Task<Result<PatientCardPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator
                    .ValidateAsync(request.PatientCardPostDTO);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    return Result<PatientCardPostDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                }

                var pc = request.PatientCardPostDTO;
                var parameters = new[]
                {
                        new SqlParameter("@PatientId", pc.PatientId),
                        new SqlParameter("@DieticianId", pc.DieticianId),
                        new SqlParameter("@SexId", pc.SexId)
                };

                try
                {
                    var result = await _context.Database.ExecuteSqlRawAsync("EXEC CreatePatientCard @PatientId, @DieticianId, @SexId", parameters) > 0;

                    if (!result)
                    {
                        return Result<PatientCardPostDTO>.Failure("Operacja nie powiodła się.");
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<PatientCardPostDTO>.Failure("Wystąpił błąd podczas tworzenia karty pacjenta.");
                }
                return Result<PatientCardPostDTO>.Success(_mapper.Map<PatientCardPostDTO>(pc));
            }
        }
    }
}