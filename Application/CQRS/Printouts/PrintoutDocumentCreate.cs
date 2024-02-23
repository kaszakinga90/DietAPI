using Application.Core;
using Application.DTOs.PrintoutsDTO;
using DietDB;
using MediatR;
using Xceed.Words.NET;

namespace Application.CQRS.Printouts
{
    // Klasa odpowiedzialna za tworzenie dokumentów wydruku.
    public class PrintoutDocumentCreate : IRequest<Result<string>>
    {
        // Komenda reprezentująca żądanie stworzenia dokumentu.
        public class Command : IRequest<Result<string>>
        {
            // Dane niezbędne do stworzenia dokumentu.
            public PrintoutDocumentPostDTO Data { get; set; }
        }

        // Handler obsługujący logikę tworzenia dokumentu.
        public class Handler : IRequestHandler<Command, Result<string>>
        {
            // Kontekst bazy danych.
            private readonly DietContext _context;

            // Konstruktor inicjalizujący handlera.
            public Handler(DietContext context)
            {
                _context = context;
            }

            // Metoda obsługująca żądanie stworzenia dokumentu.
            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                // Pobranie danych wejściowych.
                var incomingData = request.Data;

                // Pobranie szablonu wydruku i użytkownika z bazy danych.
                var printoutTemplate = await _context.PrintoutsDb.FindAsync(incomingData.Id);
                var user = await _context.Users.FindAsync(incomingData.DieticianId);

                // Sprawdzenie, czy znaleziono szablon i użytkownika.
                if (printoutTemplate == null || user == null)
                {
                    return Result<string>.Failure("Szablon lub użytkownik nie został znaleziony.");
                }

                // Generowanie dokumentu Word.
                string filePath = GenerateWordDocument(printoutTemplate.Data, user.FirstName, user.LastName);

                return Result<string>.Success(filePath);
            }

            // Prywatna metoda do generowania dokumentu Word.
            private string GenerateWordDocument(string templateData, string firstName, string lastName)
            {
                // Tworzenie nowego dokumentu Word.
                var doc = DocX.Create("output.docx");

                // Wstawianie danych do szablonu.
                templateData = templateData.Replace("{FirstName}", firstName ?? string.Empty);
                templateData = templateData.Replace("{LastName}", lastName ?? string.Empty);

                // Wstawienie sformatowanego szablonu do dokumentu.
                doc.InsertParagraph(templateData);

                // Zapisanie dokumentu i zwrot ścieżki pliku.
                string filePath = Path.Combine("test", "output.docx");
                doc.SaveAs(filePath);

                return filePath;
            }
        }
    }
}