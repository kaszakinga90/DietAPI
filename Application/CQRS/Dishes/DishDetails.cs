using Application.Core;
using Application.DTOs.DishDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Dishes
{
    public class DishDetails
    {
        public class Query : IRequest<Result<DishGetDTO>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DishGetDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<DishGetDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dish = await _context.DishesDb
                        .Include(dish => dish.Recipe)
                        .ThenInclude(recipe => recipe.Steps)
                        //.Include(d => d.DishIngredients)
                        //.ThenInclude(dr=>dr.N)
                        //.Include(d => d.DishFoodCatalogs)
                        .FirstOrDefaultAsync(d => d.Id == request.Id);

                    if (dish == null)
                    {
                        return Result<DishGetDTO>.Failure("no results");
                    }
                    var recipeSteps = await _context.RecipeStepsDb
               .Where(r => r.RecipeId == dish.Id)
               .ToListAsync();

                    var dishDTO = _mapper.Map<DishGetDTO>(dish);
                    return Result<DishGetDTO>.Success(dishDTO);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<DishGetDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }

        }
    }
}