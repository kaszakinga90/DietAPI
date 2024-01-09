using Application.Core;
using Application.DTOs.SpecializationDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Functionality;
using System.Diagnostics;

namespace Application.CQRS.Specializations
{
    public class DieteticianSpecializationCreate
    {
        public class Command : IRequest<Result<DieteticianSpecializationPostDTO>>
        {
            public DieteticianSpecializationPostDTO DieteticianSpecializationPostDTOs { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<DieteticianSpecializationPostDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<DieteticianSpecializationPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var ds = _mapper.Map<DieticianSpecialization>(request.DieteticianSpecializationPostDTOs);

                if (ds == null)
                {
                    return Result<DieteticianSpecializationPostDTO>.Failure("No results for variable: ds");
                }

                var specializationName = await _context.SpecializationsDb
                         .Where(s => s.Id == ds.SpecializationId)
                         .Select(s => s.SpecializationName)
                         .FirstOrDefaultAsync(cancellationToken);

                if (specializationName == null)
                {
                    return Result<DieteticianSpecializationPostDTO>.Failure("No results for variable: specializationName");
                }

                var resultDto = new DieteticianSpecializationPostDTO
                {
                    DieticianId = ds.DieticianId,
                    SpecializationId = ds.SpecializationId,
                    SpecializationName = specializationName
                };

                _context.DieticianSpecialization.Add(ds);

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<DieteticianSpecializationPostDTO>.Failure("Operacja nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<DieteticianSpecializationPostDTO>.Failure("Wystąpił błąd podczas dodawania dietician specialization.");
                }

                return Result<DieteticianSpecializationPostDTO>.Success(_mapper.Map<DieteticianSpecializationPostDTO>(resultDto));
            }
        }
    }
}