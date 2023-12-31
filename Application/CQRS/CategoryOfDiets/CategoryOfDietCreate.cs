﻿using DietDB;
using MediatR;
using ModelsDB.Functionality;

namespace Application.CQRS.CategoryOfDiets
{
    public class CategoryOfDietCreate
    {
        public class Command : IRequest
        {
            public CategoryOfDiet CategoryOfDiet { get; set; }
        }
        public class Hendler : IRequestHandler<Command>
        {
            private readonly DietContext _context;

            public Hendler(DietContext context)
            {
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                _context.CategoryOfDietsDb.Add(request.CategoryOfDiet);

                await _context.SaveChangesAsync();
            }
        }
    }
}
