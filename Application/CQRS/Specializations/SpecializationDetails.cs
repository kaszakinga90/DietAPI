using Application.Core;
using Application.DTOs.FoodCatalogDTO;
using Application.DTOs.SpecializationDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Specializations
{
    public class SpecializationDetails
    {
        public class Query : IRequest<Result<SpecializationGetDTO>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<SpecializationGetDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<SpecializationGetDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var specialization = await _context.SpecializationsDb
                    .SingleOrDefaultAsync(x => x.Id == request.Id);

                    if (specialization == null)
                    {
                        return Result<SpecializationGetDTO>.Failure("specialization o podanym id nie został odnaleziony");
                    }

                    return Result<SpecializationGetDTO>.Success(_mapper.Map<SpecializationGetDTO>(specialization));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<SpecializationGetDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}