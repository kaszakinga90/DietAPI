using Application.Core;
using Application.DTOs.MealDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Meals
{
    public class MealList
    {
        public class Query : IRequest<Result<List<MealGetDTO>>>
        {
            public class Handler : IRequestHandler<Query, Result<List<MealGetDTO>>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<List<MealGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
                {
                    try
                    {
                        var mealsList = await _context.MealsDb
                        .Select(m => new MealGetDTO
                        {
                            Id = m.Id,
                            Name = m.Name,
                        })
                        .ToListAsync(cancellationToken);

                        return Result<List<MealGetDTO>>.Success(mealsList);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<List<MealGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                    }
                }
            }
        }
    }
}