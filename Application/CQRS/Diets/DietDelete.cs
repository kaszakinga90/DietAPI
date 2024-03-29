﻿using Application.Core;
using Application.DTOs.DietDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Diets
{
    public class DietDelete
    {
        public class Command : IRequest<Result<DietDeleteDTO>>
        {
            public int DietId { get; set; }

            public class Handler : IRequestHandler<Command, Result<DietDeleteDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<DietDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
                {

                    var diet = await _context.DietsDb
                             .Where(fc => fc.Id == request.DietId)
                             .SingleOrDefaultAsync();

                    if (diet == null)
                    {
                        return Result<DietDeleteDTO>.Failure("Nie znaleziono diety.");
                    }

                    var dietPatientEntry = await _context.DietPatientsDb
                                            .FirstOrDefaultAsync(dp => dp.DietId == request.DietId);

                    if (dietPatientEntry != null)
                    {
                        return Result<DietDeleteDTO>.Failure("Nie można usunąć diety, ponieważ istnieje powiązanie z pacjentem.");
                    }

                    var mealTimeToXYAxisEntries = await _context.MealTimesDb
                        .Where(mt => mt.DietId == request.DietId)
                        .ToListAsync();

                    if (mealTimeToXYAxisEntries.Any())
                    {
                        _context.MealTimesDb.RemoveRange(mealTimeToXYAxisEntries);
                    }
                    _context.DietsDb.Remove(diet);

                    try
                    {
                        var result = await _context.SaveChangesAsync() > 0;
                        if (!result)
                        {
                            return Result<DietDeleteDTO>.Failure("Operacja nie powiodła się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<DietDeleteDTO>.Failure("Wystąpił błąd podczas usuwania diet.");
                    }

                    return Result<DietDeleteDTO>.Success(_mapper.Map<DietDeleteDTO>(diet));
                }
            }
        }
    }
}