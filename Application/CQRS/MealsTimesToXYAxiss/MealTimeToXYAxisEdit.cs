using Application.Core;
using Application.DTOs.MealTimeToXYAxisDTO;
using Application.Validators.MealTimeToXYAxis;
using AutoMapper;
using DietDB;
using MediatR;
using System.Diagnostics;

// TODO : walidacja
namespace Application.CQRS.MealsTimesToXYAxiss
{
    public class MealTimeToXYAxisEdit
    {
        public class Command : IRequest<Result<MealTimeToXYAxisEditDTO>>
        {
            public MealTimeToXYAxisEditDTO MealTimeToXYAxisEditDTO{ get; set; }
            public class Handler : IRequestHandler<Command, Result<MealTimeToXYAxisEditDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;
                private readonly MealTimeToXYAxisUpdateValidator _validator;

                public Handler(DietContext context, IMapper mapper, MealTimeToXYAxisUpdateValidator validator)
                {
                    _context = context;
                    _mapper = mapper;
                    _validator = validator;
                }

                public async Task<Result<MealTimeToXYAxisEditDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    //var validationResult = await _validator
                    //.ValidateAsync(request.MealTimeToXYAxisEditDTO);

                    //if (!validationResult.IsValid)
                    //{
                    //    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    //    return Result<MealTimeToXYAxisEditDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                    //}

                    var mealShedule = await _context.MealTimesDb
                        .FindAsync(new object[] { request.MealTimeToXYAxisEditDTO.Id });

                    if (mealShedule == null)
                    {
                        return Result<MealTimeToXYAxisEditDTO>.Failure("Posilek o podanym ID nie został znaleziony.");
                    }

                    _mapper.Map(request.MealTimeToXYAxisEditDTO, mealShedule);

                    try
                    {
                        var result = await _context.SaveChangesAsync() > 0;
                        if (!result)
                        {
                            return Result<MealTimeToXYAxisEditDTO>.Failure("Edycja posilku nie powiodła się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<MealTimeToXYAxisEditDTO>.Failure("Wystąpił błąd podczas edycji posilku. " + ex);
                    }

                    return Result<MealTimeToXYAxisEditDTO>.Success(_mapper.Map<MealTimeToXYAxisEditDTO>(mealShedule));
                }
            }
        }
    }
}