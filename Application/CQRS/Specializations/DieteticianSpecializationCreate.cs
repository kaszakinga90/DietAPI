using Application.DTOs.SpecializationDTO;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;

namespace Application.CQRS.Specializations
{
    public class DieteticianSpecializationCreate
    {
        public class Command : IRequest
        {
            public DieteticianSpecializationPostDTO DieteticianSpecializationPostDTOs { get; set; }
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
                var ds = _mapper.Map<DieticianSpecialization>(request.DieteticianSpecializationPostDTOs);

                _context.DieticianSpecialization.Add(ds);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
