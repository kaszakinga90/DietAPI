using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;
using ModelsDB;
using Application.DTOs.OfficeDTO;
using Application.DTOs.AddressDTO;

namespace Application.Functionality
{
    public class OfficeCreate
    {
        public class Command : IRequest<OfficePostDTO>
        {
            public OfficePostDTO OfficePostDTO { get; set; }
            public AddressPostDTO AddressPostDTO { get; set; }
            public int DieticianId { get; set; }
        }

        public class Handler : IRequestHandler<Command, OfficePostDTO>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<OfficePostDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                // Tworzenie adresu
                var address = _mapper.Map<Address>(request.AddressPostDTO);
                _context.AddressesDb.Add(address);
                await _context.SaveChangesAsync(cancellationToken);

                // Tworzenie biura
                var office = new Office
                {
                    OfficeName = request.OfficePostDTO.OfficeName,
                    AddressId = address.Id
                };
                _context.OfficesDb.Add(office);
                await _context.SaveChangesAsync(cancellationToken);

                // Tworzenie rekordu w tabeli DieticianOffice
                var dieticianOffice = new DieticianOffice
                {
                    DieticianId = request.DieticianId,
                    OfficeId = office.Id
                };
                _context.DieticianOffices.Add(dieticianOffice);
                await _context.SaveChangesAsync(cancellationToken);

                var officeDto = _mapper.Map<OfficePostDTO>(office); 
                return officeDto;

            }
        }
    }
}
