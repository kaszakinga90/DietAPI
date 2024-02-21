using Application.Core;
using Application.DTOs.AdminDTO;
using Application.DTOs.CountryStateDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Admins
{
    public class AdminNoPaginationList
    {
        public class Query : IRequest<Result<List<AdminGetDTO>>> { }

        public class Handler : IRequestHandler<Query, Result<List<AdminGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<AdminGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var AdminList = await _context.AdminsDb
                          .Select(m => new AdminGetDTO
                          {
                              Id = m.Id,
                              AdminName = m.FirstName + " " + m.LastName,
                          })
                          .ToListAsync(cancellationToken);

                    return Result<List<AdminGetDTO>>.Success(AdminList);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<AdminGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}