using Application.Core;
using Application.DTOs.RoleDTO;
using Application.FiltersExtensions.Roles;
using DietDB;
using MediatR;

namespace Application.CQRS.Roles
{
    public class RoleList
    {
        public class Query : IRequest<Result<PagedList<RoleGetDTO>>>
        {
            public RoleParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<RoleGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PagedList<RoleGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var rolesList = _context.Roles
                    .Select(a => new RoleGetDTO
                    {
                        Id = a.Id,
                        Name = a.Name,
                    })
                    .AsQueryable();

                if (rolesList == null)
                {
                    return Result<PagedList<RoleGetDTO>>.Failure("no results.");
                }

                return Result<PagedList<RoleGetDTO>>.Success(
                    await PagedList<RoleGetDTO>.CreateAsync(rolesList, request.Params.PageNumber, request.Params.PageSize)
                    );
            }
        }
    }
}
