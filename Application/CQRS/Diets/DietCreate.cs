using Application.DTOs.MealTimeToXYAxisDTO;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB;
using ModelsDB.Functionality;

namespace Application.CQRS.Diets
{
    public class DietCreate
    {
        public class Command : IRequest
        {
            public DietDTO DietDTO { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var diet = _mapper.Map<Diet>(request.DietDTO);

                _context.DietsDb.Add(diet);
                await _context.SaveChangesAsync(cancellationToken);

                //if (request.DietDTO.MealTimesToXYAxisDTO != null && request.DietDTO.MealTimesToXYAxisDTO.Any())
                //{
                //    foreach (var mealTimeDto in request.DietDTO.MealTimesToXYAxisDTO)
                //    {
                //        var mealTime = _mapper.Map<MealTimeToXYAxis>(mealTimeDto);

                //        // Przypisz DietId do MealTimeToXYAxis
                //        mealTime.DietId = diet.Id;
                //        //mealTime.DishId = null;

                //        _context.MealTimesDb.Add(mealTime);

                //        Console.WriteLine("=================================================================");
                //        Console.WriteLine("Mealtime dodawany do contextu ======= >   " + mealTime.ToString());
                //        Console.WriteLine("=================================================================");
                //    }
                //}

                //await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
