using Application.Core;
using Application.DTOs.UsersDTO;
using Application.FiltersExtensions.UserRoles;
using DietDB;
using MediatR;

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
                var rolesList = _context.Users
                    .Select(a => new UserWithRoleGetDTO
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Email = a.Email
                    })
                    .AsQueryable();

                if (rolesList == null)
                {
                    return Result<PagedList<UserWithRoleGetDTO>>.Failure("no results.");
                }

                return Result<PagedList<UserWithRoleGetDTO>>.Success(
                    await PagedList<UserWithRoleGetDTO>.CreateAsync(rolesList, request.Params.PageNumber, request.Params.PageSize)
                    );
            }
        }
    }
}
