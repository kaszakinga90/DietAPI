using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS
{
    public class MessageToPatientCreate
    {
        public class Command : IRequest
        {
            public MessageToPatientDTO MessageDTO { get; set; }
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
                var message = _mapper.Map<Message>(request.MessageDTO);

                // Przypisuj wiadomość do pacjenta
                var patient = await _context.Patients.FindAsync(request.MessageDTO.PatientId);
                if (patient == null)
                {
                    // Rzuć błąd jeśli pacjent nie został znaleziony
                    throw new Exception("Pacjent nie został znaleziony");
                }

                _context.Messages.Add(message);
                await _context.SaveChangesAsync();  // Tu możesz najpierw zapisać wiadomość, aby miała przypisane ID

                // Tworzenie obiektu MessagePatient
                var messagePatient = new MessagePatient
                {
                    PatientId = patient.Id,
                    MessageId = message.Id
                };

                _context.MessagePatients.Add(messagePatient);
                await _context.SaveChangesAsync();
            }
        }
    }
}