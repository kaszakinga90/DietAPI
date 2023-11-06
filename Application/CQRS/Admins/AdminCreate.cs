using DietDB;
using MediatR;
using ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Admins
{
    /// <summary>
    /// Zawiera klasy służące do tworzenia nowego admina w bazie danych.
    /// </summary>
    public class AdminCreate
    {
        /// <summary>
        /// Reprezentuje komendę służącą do tworzenia nowego admina.
        /// </summary>
        public class Command : IRequest
        {
            /// <summary>
            /// Pobiera lub ustawia model admina, który ma zostać dodany do bazy danych.
            /// </summary>
            public Admin Admin { get; set; }
        }

        /// <summary>
        /// Obsługuje proces dodawania nowego admina do bazy danych.
        /// </summary>
        public class Handler : IRequestHandler<Command>
        {
            private readonly DietContext _context;

            /// <summary>
            /// Inicjuje nową instancję klasy <see cref="Handler"/> z podanym kontekstem bazy danych.
            /// </summary>
            /// <param name="context">Kontekst bazy danych do obsługi adminów.</param>
            public Handler(DietContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Przetwarza komendę tworzenia nowego admina w bazie danych.
            /// </summary>
            /// <param name="request">Komenda do przetworzenia.</param>
            /// <param name="cancellationToken">Token anulowania operacji.</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                _context.AdminsDb.Add(request.Admin);
                await _context.SaveChangesAsync();
            }
        }
    }
}
