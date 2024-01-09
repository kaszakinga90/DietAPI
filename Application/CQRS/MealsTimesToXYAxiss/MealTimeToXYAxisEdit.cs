using Application.Core;
using Application.DTOs.MealTimeToXYAxisDTO;
using AutoMapper;
using DietDB;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.MealsTimesToXYAxiss
{
    public class MealTimeToXYAxisEdit
    {
        public class Command : IRequest<Result<MealTimeToXYAxisEditDTO>>
        {
            public MealTimeToXYAxisEditDTO MealTimeToXYAxis{ get; set; }
            public class Handler : IRequestHandler<Command, Result<MealTimeToXYAxisEditDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<MealTimeToXYAxisEditDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var mealShedule = await _context.MealTimesDb.FindAsync(new object[] { request.MealTimeToXYAxis.Id }, cancellationToken);
                    if (mealShedule == null)
                    {
                        return Result<MealTimeToXYAxisEditDTO>.Failure("Posilek o podanym ID nie został znaleziony.");
                    }

                    _mapper.Map(request.MealTimeToXYAxis, mealShedule);

                    try
                    {
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
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