using Application.Core;
using Application.DTOs.PrintoutsDTO;
using DietDB;
using MediatR;
using Xceed.Words.NET;

namespace Application.CQRS.Printouts
{
    // Klasa służąca do tworzenia dokumentów wydruków parametryzowanych.
    public class PrintoutDocumentCreate : IRequest<Result<string>>
    {
        // Komenda reprezentująca żądanie utworzenia wydruku
        public class Command : IRequest<Result<string>>
        {
            public PrintoutDocumentPostDTO Data { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var incomingData = request.Data;

                var printoutTemplate = await _context.PrintoutsDb.FindAsync(incomingData.Id);
                var user = await _context.Users.FindAsync(incomingData.DieticianId);

                if (printoutTemplate == null || user == null)
                {
                    return Result<string>.Failure("Szablon lub użytkownik nie został znaleziony.");
                }

                // Wygenerowanie pliku word z danymi
                string filePath = GenerateWordDocument(printoutTemplate.Data, user.FirstName, user.LastName);

                return Result<string>.Success(filePath);
            }

            // Metoda z logiką generowania dokumentu i jego zawartości
            private string GenerateWordDocument(string templateData, string firstName, string lastName)
            {
                // Utworzenie nowego dokumentu
                var doc = DocX.Create("output.docx");

                // Zamiana danych na te przekazane
                templateData = templateData.Replace("{FirstName}", firstName ?? string.Empty);
                templateData = templateData.Replace("{LastName}", lastName ?? string.Empty);

                // Wstawienie sformatowanego szablonu do dokumentu
                doc.InsertParagraph(templateData);

                // Zapisanie pliku i pobranie ścieżki do niego
                string filePath = Path.Combine("test", "output.docx");
                doc.SaveAs(filePath);

                return filePath;
            }
        }
    }
}