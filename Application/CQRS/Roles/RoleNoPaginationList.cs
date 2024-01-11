using Application.Core;
using Application.DTOs.RoleDTO;
using Application.FiltersExtensions.Roles;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Roles
{
    public class RoleNoPaginationList
    {
        public class Query : IRequest<Result<List<RoleGetDTO>>>
        {
            public RoleParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<RoleGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<RoleGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var rolesList = await _context.Roles
                    .Select(a => new RoleGetDTO
                    {
                        Id = a.Id,
                        Name = a.Name,
                    })
                    .ToListAsync(cancellationToken);

                    return Result<List<RoleGetDTO>>.Success(rolesList);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<RoleGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}