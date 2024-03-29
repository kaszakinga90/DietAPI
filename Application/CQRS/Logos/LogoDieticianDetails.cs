﻿using Application.Core;
using Application.DTOs.LogoDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Logos
{
    public class LogoDieticianDetails
    {
        public class Query : IRequest<Result<LogoGetDTO>>
        {
            public int DieticianId { get; set; }

            public class Handler : IRequestHandler<Query, Result<LogoGetDTO>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<LogoGetDTO>> Handle(Query request, CancellationToken cancellationToken)
                {
                    try
                    {
                        var logo = await _context.LogosDb
                        .Where(m => m.DieticianId == request.DieticianId)
                        .Select(m => new LogoGetDTO
                        {
                            Id = m.Id,
                            PictureUrl = m.PictureUrl
                        })
                        .FirstOrDefaultAsync();

                        if (logo == null)
                        {
                            return Result<LogoGetDTO>.Failure("Nie znaleziono logo.");
                        }

                        return Result<LogoGetDTO>.Success(logo);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<LogoGetDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                    } 
                }
            }
        }
    }
}