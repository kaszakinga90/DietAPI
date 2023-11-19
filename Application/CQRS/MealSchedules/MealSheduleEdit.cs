using Application.Core;
using Application.DTOs.MealScheduleDTO;
using AutoMapper;
using DietDB;
using MediatR;

namespace Application.CQRS.MealSchedules
{
    public class MealSheduleEdit
    {
        public class Command : IRequest<Result<MealScheduleEditDTO>>
        {
            public MealScheduleEditDTO MealShedule { get; set; }
            public class Handler : IRequestHandler<Command, Result<MealScheduleEditDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<MealScheduleEditDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var mealShedule = await _context.MealSchedulesDb.FindAsync(new object[] { request.MealShedule.Id }, cancellationToken);
                    if (mealShedule == null)
                    {
                        return Result<MealScheduleEditDTO>.Failure("Posilek o podanym ID nie został znaleziony.");
                    }

                    _mapper.Map(request.MealShedule, mealShedule);

                    try
                    {
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        if (!result)
                        {
                            return Result<MealScheduleEditDTO>.Failure("Edycja posilku nie powiodła się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        return Result<MealScheduleEditDTO>.Failure("Wystąpił błąd podczas edycji posilku.");
                    }

                    return Result<MealScheduleEditDTO>.Success(_mapper.Map<MealScheduleEditDTO>(mealShedule));
                }
            }
        }
    }
}