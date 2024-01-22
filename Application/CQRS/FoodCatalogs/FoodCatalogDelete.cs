using Application.Core;
using Application.DTOs.FoodCatalogDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB;
using System.Diagnostics;

namespace Application.CQRS.FoodCatalogs
{
    public class FoodCatalogDelete
    {
        public class Command : IRequest<Result<FoodCatalogDeleteDTO>>
        {
            public int FoodCatalogId { get; set; }
            public FoodCatalogDeleteDTO FoodCatalogDeleteDTO { get; set; }

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
                    var foodCatalogQuery = _context.FoodCatalogsDb.AsQueryable();

                    FoodCatalog foodCatalog = null;
                    if (request.FoodCatalogDeleteDTO.DieticianId != null)
                    {
                        foodCatalog = await foodCatalogQuery
                            .Where(fc => fc.Id == request.FoodCatalogId && fc.DieticianId == request.FoodCatalogDeleteDTO.DieticianId)
                            .SingleOrDefaultAsync(cancellationToken);

                        if (foodCatalog == null)
                        {
                            return Result<FoodCatalogDeleteDTO>.Failure("foodCatalog not found.");
                        }


                        var foodCatalogAll = await foodCatalogQuery
                            .Where(fc => fc.DieticianId == request.FoodCatalogDeleteDTO.DieticianId && fc.CatalogName == "Wszystkie")
                            .SingleOrDefaultAsync(cancellationToken);

                        if (foodCatalogAll == null)
                        {
                            return Result<FoodCatalogDeleteDTO>.Failure("foodCatalogAll for dietician not found.");
                        }

                        foodCatalog.isActive = false;

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
                    else
                    {
                        var foodCatalogIdForNoDietician = await foodCatalogQuery
                            .Where(fc => fc.DieticianId == null && fc.Id == request.FoodCatalogId)
                            .SingleOrDefaultAsync(cancellationToken);

                        if (foodCatalogIdForNoDietician == null)
                        {
                            return Result<FoodCatalogDeleteDTO>.Failure("foodCatalog for no dietician not found.");
                        }

                        foodCatalogIdForNoDietician.isActive = false;

                        var dishFoodCatalogs = await _context.DishFoodCatalogsDb
                            .Where(df => df.FoodCatalogId == foodCatalogIdForNoDietician.Id)
                            .ToListAsync(cancellationToken);

                        if (dishFoodCatalogs.Any())
                        {
                            foreach (var dishFoodCatalog in dishFoodCatalogs)
                            {
                                dishFoodCatalog.FoodCatalogId = 1;
                            }
                        }

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

                        return Result<FoodCatalogDeleteDTO>.Success(_mapper.Map<FoodCatalogDeleteDTO>(foodCatalogIdForNoDietician));
                    }
                }
            }
        }
    }
}