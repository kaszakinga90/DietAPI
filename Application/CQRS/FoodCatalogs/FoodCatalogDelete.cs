using Application.Core;
using Application.DTOs.FoodCatalogDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.FoodCatalogs
{
    public class FoodCatalogDelete
    {
        public class Command : IRequest<Result<FoodCatalogDeleteDTO>>
        {
            public int FoodCatalogId { get; set; }

            public class Handler : IRequestHandler<Command, Result<FoodCatalogDeleteDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<FoodCatalogDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
                {

                    var foodCatalog = await _context.FoodCatalogsDb
                             .Where(fc => fc.Id == request.FoodCatalogId)
                             .SingleOrDefaultAsync(cancellationToken);

                    if (foodCatalog == null)
                    {
                        return Result<FoodCatalogDeleteDTO>.Failure("Nie znaleziono food catalog.");
                    }

                    var foodCatalogDTO = _mapper.Map<FoodCatalogDeleteDTO>(foodCatalog);

                    if (foodCatalogDTO.DieticianId == null)
                    {

                        foodCatalogDTO.isActive = false;


                        var dishFoodCatalogs = await _context.DishFoodCatalogsDb
                            .Where(df => df.FoodCatalogId == foodCatalogDTO.Id)
                            .ToListAsync(cancellationToken);

                        if (dishFoodCatalogs.Any())
                        {
                            foreach (var dishFoodCatalog in dishFoodCatalogs)
                            {
                                dishFoodCatalog.FoodCatalogId = 1;
                            }
                        }
                    }
                    else
                    {
                        var foodCatalogAll = await _context.FoodCatalogsDb
                        .Where(fc => fc.DieticianId == foodCatalogDTO.DieticianId && fc.CatalogName == "Wszystkie")
                        .SingleOrDefaultAsync(cancellationToken);

                        if (foodCatalogAll == null)
                        {
                            return Result<FoodCatalogDeleteDTO>.Failure("Nie znaleziono foodCatalogAll for dietician.");
                        }

                        foodCatalogDTO.isActive = false;

                        var dishFoodCatalogs = await _context.DishFoodCatalogsDb
                            .Where(df => df.FoodCatalogId == foodCatalog.Id)
                            .ToListAsync(cancellationToken);

                        if (dishFoodCatalogs.Any())
                        {
                            foreach (var dishFoodCatalog in dishFoodCatalogs)
                            {
                                dishFoodCatalog.FoodCatalogId = foodCatalogAll.Id;
                            }
                        }
                    }

                    _mapper.Map(foodCatalogDTO, foodCatalog);

                    try
                    {
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        if (!result)
                        {
                            return Result<FoodCatalogDeleteDTO>.Failure("Operacja nie powiodła się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<FoodCatalogDeleteDTO>.Failure("Wystąpił błąd podczas usuwania foodCatalog.");
                    }

                    return Result<FoodCatalogDeleteDTO>.Success(_mapper.Map<FoodCatalogDeleteDTO>(foodCatalog));
                }
            }
        }
    }
}