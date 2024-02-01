using Application.Core;
using Application.DTOs.DishDTO;
using Application.FiltersExtensions.Dishes;
using DietDB;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Dishes
{
    public class AllDishListWithPaginationNoDIetician
    {
        public class Query : IRequest<Result<PagedList<DishGetDTO>>>
        {
            public DishParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<DishGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PagedList<DishGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dishesList = _context.DishesDb
                    .Where(d => d.DieticianId == null)
                    .Select(d => new DishGetDTO
                    {
                        Id = d.Id,
                        Name = d.Name,
                    })
                    .AsQueryable();

                    dishesList = dishesList.DishSearch(request.Params.SearchTerm);
                    dishesList = dishesList.DishSort(request.Params.OrderBy);
                    dishesList = dishesList.DishFilter(request.Params.DishNames);

                    return Result<PagedList<DishGetDTO>>.Success(
                        await PagedList<DishGetDTO>.CreateAsync(dishesList, request.Params.PageNumber, request.Params.PageSize)
                        );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<PagedList<DishGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}