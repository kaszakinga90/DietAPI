using Application.Core;
using Application.DTOs.MealScheduleDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Functionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.MealSchedules
{
    public class MealScheduleDetails
    {
        public class Query : IRequest<Result<List<MealScheduleGetDTO>>>
        {
            public int DietId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<MealScheduleGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<MealScheduleGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var diet = await _context.MealSchedulesDb
                    .Where(d => d.DietId == request.DietId)
                    .Select(d => new MealScheduleGetDTO
                    {
                        Id = d.Id,
                        DietId = d.DietId,
                        DishId = d.DishId,
                        MealTime = d.MealTime,
                        DishName=d.Dish.Name,
                    })
                    .ToListAsync(cancellationToken);

                return Result<List<MealScheduleGetDTO>>.Success(diet);
            }
        }
    }
}

