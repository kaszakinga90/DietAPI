﻿using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LayoutPhotos
{
    public class LayoutPhotoEdit
    {
        public class Command : IRequest
        {
            public LayoutPhoto LayoutPhoto { get; set; }
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
                var LayoutPhoto = await _context.LayoutPhotos.FindAsync(request.LayoutPhoto.Id);
                _mapper.Map(request.LayoutPhoto, LayoutPhoto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
