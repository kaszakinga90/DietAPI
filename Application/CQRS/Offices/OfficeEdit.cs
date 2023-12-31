﻿using Application.Core;
using Application.DTOs.OfficeDTO;
using AutoMapper;
using DietDB;
using MediatR;

namespace Application.CQRS.Offices
{
    public class OfficeEdit
    {
        public class Command : IRequest<Result<OfficeEditDTO>>
        {
            public OfficeEditDTO OfficeEdit { get; set; }
            public class Handler : IRequestHandler<Command, Result<OfficeEditDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<OfficeEditDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var office = await _context.OfficesDb.FindAsync(new object[] { request.OfficeEdit.Id }, cancellationToken);
                    if (office == null)
                    {
                        return Result<OfficeEditDTO>.Failure("Biuro o podanym ID nie został znaleziony.");
                    }

                    _mapper.Map(request.OfficeEdit, office);

                    try
                    {
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        if (!result)
                        {
                            return Result<OfficeEditDTO>.Failure("Biuro pacjenta nie powiodła się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        return Result<OfficeEditDTO>.Failure("Wystąpił błąd podczas edycji biura.");
                    }

                    return Result<OfficeEditDTO>.Success(_mapper.Map<OfficeEditDTO>(office));
                }
            }
        }
    }
}