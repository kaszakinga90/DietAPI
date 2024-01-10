using Application.Core;
using Application.DTOs.UsersDTO;
using Application.FiltersExtensions.UserRoles;
using DietDB;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.UserRoles
{
    public class UserRoleList
    {
        public class Query : IRequest<Result<PagedList<UserWithRoleGetDTO>>>
        {
            public UserWithRoleParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<UserWithRoleGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PagedList<UserWithRoleGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var rolesList = _context.Users
                    .Select(a => new UserWithRoleGetDTO
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Email = a.Email
                    })
                    .AsQueryable();

                    return Result<PagedList<UserWithRoleGetDTO>>.Success(
                        await PagedList<UserWithRoleGetDTO>.CreateAsync(rolesList, request.Params.PageNumber, request.Params.PageSize)
                        );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<PagedList<UserWithRoleGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}