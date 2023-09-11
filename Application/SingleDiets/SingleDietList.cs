using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB;
using ModelsDB.Functionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SingleDiets
{
    public class SingleDietList
    {
        public class Query : IRequest<List<SingleDiet>> { }

        public class Handler : IRequestHandler<Query, List<SingleDiet>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<List<SingleDiet>> Handle(Query request, CancellationToken cancellationToken)
            {
                var singleList = await _context.SingleDiet
                    .Include(a => a.DayWeek)
                    .Include(b => b.MealTimes)
                    .ToListAsync(cancellationToken);

                return singleList;
            }
        }
    }
}
