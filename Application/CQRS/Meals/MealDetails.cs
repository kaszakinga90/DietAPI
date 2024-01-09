using Application.Core;
using Application.DTOs.MealDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Meals
{
    public class MealDetails
    {
        public class Query : IRequest<Result<MealGetDTO>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<MealGetDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<MealGetDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var meal = await _context.MealsDb
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (meal == null)
                {
                    return Result<MealGetDTO>.Failure("Food catalog o podanym id nie został odnaleziony");
                }

                return Result<MealGetDTO>.Success(_mapper.Map<MealGetDTO>(meal));
            }
        }
    }
}