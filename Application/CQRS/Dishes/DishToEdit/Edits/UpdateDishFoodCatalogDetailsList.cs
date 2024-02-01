using Application.Core;
using Application.DTOs.DishDetailsToEditDTO;
using Application.Validators.DishEditDetails;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Functionality;
using System.Diagnostics;

namespace Application.CQRS.Dishes.DishToEdit.Edits
{
    public class UpdateDishFoodCatalogDetailsList
    {
        public class Command : IRequest<Result<List<DishFoodCatalogsDetailsGetEditDTO>>>
        {
            public int DishId { get; set; }
            public List<DishFoodCatalogsDetailsGetEditDTO> DishFoodCatalogsDetailsGetEditDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<List<DishFoodCatalogsDetailsGetEditDTO>>>
        {
            private readonly DietContext _context;
            private readonly DishFoodCatalogDetailsUpdateValidator _validator;

            public Handler(DietContext context, DishFoodCatalogDetailsUpdateValidator validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<Result<List<DishFoodCatalogsDetailsGetEditDTO>>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    // Pobierz istniejące składniki dla danego DishId
                    var existingFoodCatalogs = await _context.DishFoodCatalogsDb
                        .Where(i => i.DishId == request.DishId)
                        .ToListAsync(cancellationToken);

                    // Usuń składniki, które są w bazie danych, ale nie są na liście przesłanej w żądaniu
                    var foodCatalogToDelete = existingFoodCatalogs.Where(i => !request.DishFoodCatalogsDetailsGetEditDto.Any(dto => dto.FoodCatalogId == i.FoodCatalogId)).ToList();
                    _context.DishFoodCatalogsDb.RemoveRange(foodCatalogToDelete);

                    // Dodaj nowe składniki, które nie istnieją jeszcze w bazie danych
                    var foodCatalogsToAdd = request.DishFoodCatalogsDetailsGetEditDto.Where(dto => !existingFoodCatalogs.Any(i => i.FoodCatalogId == dto.FoodCatalogId));
                    foreach (var dto in foodCatalogsToAdd)
                    {
                        var validationResult = await _validator
                        .ValidateAsync(dto, cancellationToken);

                        if (!validationResult.IsValid)
                        {
                            var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                            return Result<List<DishFoodCatalogsDetailsGetEditDTO>>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                        }

                        _context.DishFoodCatalogsDb.Add(new DishFoodCatalog
                        {
                            DishId = request.DishId,
                            FoodCatalogId = dto.FoodCatalogId
                        });
                    }

                    await _context.SaveChangesAsync(cancellationToken);

                    return Result<List<DishFoodCatalogsDetailsGetEditDTO>>.Success(request.DishFoodCatalogsDetailsGetEditDto.ToList());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<DishFoodCatalogsDetailsGetEditDTO>>.Failure("Wystąpił błąd podczas aktualizacji danych.");
                }
            }
        }
    }
}