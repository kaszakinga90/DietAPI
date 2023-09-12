﻿using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Newses
{
    public class NewsEdit
    {
        public class Command : IRequest
        {
            public News News { get; set; }
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
                var News = await _context.Newses.FindAsync(request.News.Id);
                _mapper.Map(request.News, News);
                await _context.SaveChangesAsync();
            }
        }
    }
}
