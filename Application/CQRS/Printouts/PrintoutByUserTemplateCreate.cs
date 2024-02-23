using Application.Core;
using Application.DTOs.PrintoutsDTO;
using DietDB;
using MediatR;
using Xceed.Words.NET;
using System.IO;

namespace Application.CQRS.Printouts
{
    // Klasa odpowiedzialna za tworzenie dokumentu wydruku z szablonu dostarczonego przez użytkownika.
    public class PrintoutByUserTemplateCreate : IRequest<Result<byte[]>>
    {
        // Komenda reprezentująca żądanie stworzenia dokumentu z szablonu użytkownika.
        public class Command : IRequest<Result<byte[]>>
        {
            // Dane szablonu i informacje o użytkowniku.
            public PrintoutUploadByUserPostDTO Data { get; set; }
        }

        // Handler obsługujący logikę tworzenia dokumentu.
        public class Handler : IRequestHandler<Command, Result<byte[]>>
        {
            // Kontekst bazy danych.
            private readonly DietContext _context;

            // Konstruktor inicjalizujący handlera.
            public Handler(DietContext context)
            {
                _context = context;
            }

            // Metoda obsługująca żądanie stworzenia dokumentu.
            public async Task<Result<byte[]>> Handle(Command request, CancellationToken cancellationToken)
            {
                // Pobranie danych użytkownika z bazy danych.
                var user = await _context.Users.FindAsync(request.Data.DieticianId);
                if (user == null)
                {
                    return Result<byte[]>.Failure("Użytkownik nie został znaleziony.");
                }

                // Otworzenie strumienia pamięci dla pliku szablonu.
                using (var memoryStream = new MemoryStream())
                {
                    // Kopiowanie zawartości pliku szablonu do strumienia pamięci.
                    await request.Data.TemplateFile.CopyToAsync(memoryStream);

                    // Ładowanie dokumentu Word z pamięci.
                    using (var doc = DocX.Load(memoryStream))
                    {
                        // Podmiana znaczników w szablonie danymi użytkownika.
                        doc.ReplaceText("{FirstName}", user.FirstName ?? string.Empty);
                        doc.ReplaceText("{LastName}", user.LastName ?? string.Empty);

                        // Zapisanie zmodyfikowanego dokumentu do nowego strumienia pamięci.
                        using (var output = new MemoryStream())
                        {
                            doc.SaveAs(output);
                            // Zwrócenie zawartości dokumentu jako tablicy bajtów.
                            return Result<byte[]>.Success(output.ToArray());
                        }
                    }
                }
            }
        }
    }
}