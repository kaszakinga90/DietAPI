using Application.Validators.Dietician;
using DietDB;
using MediatR;
using ModelsDB;

// TODO : poprawa aby działało na dTOsach i można było właczyć walidację
namespace Application.CQRS.Dieticians
{
    /// <summary>
    /// Zawiera klasy służące do tworzenia nowego dietetyka w bazie danych.
    /// </summary>
    public class DieticianCreate
    {
        /// <summary>
        /// Reprezentuje komendę służącą do tworzenia nowego dietetyka.
        /// </summary>
        public class Command : IRequest
        {
            /// <summary>
            /// Pobiera lub ustawia model dietetyka, który ma zostać dodany do bazy danych.
            /// </summary>
            public Dietician Dietician { get; set; }
        }

        /// <summary>
        /// Obsługuje proces dodawania nowego dietetyka do bazy danych.
        /// </summary>
        public class Handler : IRequestHandler<Command>
        {
            private readonly DietContext _context;
            private readonly DieticianCreateValidator _validator;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem bazy danych.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi dietetyków.</param>
            public Handler(DietContext context, DieticianCreateValidator validator)
            {
                _context = context;
                _validator = validator;
            }

            /// <summary>
            /// Przetwarza komendę tworzenia nowego dietetyka w bazie danych.
            /// </summary>
            /// <param name="request">Komenda do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                //var validationResult = await _validator
                //    .ValidateAsync(request.DieticianEditDTO, cancellationToken);

                //if (!validationResult.IsValid)
                //{
                //    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                //    return Result<DieticianEditDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                //}

                _context.DieticiansDb.Add(request.Dietician);
                await _context.SaveChangesAsync();
            }
        }
    }
}
