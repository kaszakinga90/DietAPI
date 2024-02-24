using Application.Core;
using Application.DTOs.PrintoutsDTO;
using DietDB;
using MediatR;
using Xceed.Words.NET;

namespace Application.CQRS.Printouts
{
    /// <summary>
    /// Klasa służąca do tworzenia dokumentów wydruków parametryzowanych
    /// </summary>
    public class PrintoutDocumentCreate : IRequest<Result<string>>
    {
        /// <summary>
        /// Komenda reprezentująca żądanie utworzenia wydruku
        /// </summary>
        public class Command : IRequest<Result<byte[]>>
        {
            public PrintoutDocumentPostDTO Data { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<byte[]>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<byte[]>> Handle(Command request, CancellationToken cancellationToken)
            {
                var incomingData = request.Data;

                var printoutTemplate = await _context.PrintoutsDb.FindAsync(incomingData.Id);
                var user = await _context.Users.FindAsync(incomingData.DieticianId);

                if (printoutTemplate == null || user == null)
                {
                    return Result<byte[]>.Failure("Szablon lub użytkownik nie został znaleziony.");
                }

                // Wygenerowanie pliku word z danymi
                byte[] fileBytes = GenerateWordDocument(printoutTemplate.Data, user.FirstName, user.LastName);

                return Result<byte[]>.Success(fileBytes);
            }
            /// <summary>
            /// Metoda z logiką generowania dokumentu i jego zawartości
            /// </summary>
            private byte[] GenerateWordDocument(string templateData, string firstName, string lastName)
            {
                // Utworzenie nowego dokumentu
                var doc = DocX.Create("output.docx");

                // Zamiana danych na te przekazane
                templateData = templateData.Replace("{FirstName}", firstName ?? string.Empty);
                templateData = templateData.Replace("{LastName}", lastName ?? string.Empty);

                // Wstawienie sformatowanego szablonu do dokumentu
                doc.InsertParagraph(templateData);

                // Zapisanie pliku jako tablicy bajtów
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    doc.SaveAs(memoryStream);
                    return memoryStream.ToArray();
                }
            }




            //public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            //{
            //    var incomingData = request.Data;

            //    var printoutTemplate = await _context.PrintoutsDb.FindAsync(incomingData.Id);
            //    var user = await _context.Users.FindAsync(incomingData.DieticianId);

            //    if (printoutTemplate == null || user == null)
            //    {
            //        return Result<string>.Failure("Szablon lub użytkownik nie został znaleziony.");
            //    }

            //    // Wygenerowanie pliku word z danymi
            //    string filePath = GenerateWordDocument(printoutTemplate.Data, user.FirstName, user.LastName);

            //    return Result<string>.Success(filePath);
            //}

            /// <summary>
            /// Metoda z logiką generowania dokumentu i jego zawartości
            /// </summary>
            //private string GenerateWordDocument(string templateData, string firstName, string lastName)
            //{
            //    // Utworzenie nowego dokumentu
            //    var doc = DocX.Create("output.docx");

            //    // Zamiana danych na te przekazane
            //    templateData = templateData.Replace("{FirstName}", firstName ?? string.Empty);
            //    templateData = templateData.Replace("{LastName}", lastName ?? string.Empty);

            //    // Wstawienie sformatowanego szablonu do dokumentu
            //    doc.InsertParagraph(templateData);

            //    // Zapisanie pliku i pobranie ścieżki do niego
            //    string filePath = Path.Combine("test", "output.docx");
            //    doc.SaveAs(filePath);

            //    return filePath;
            //}
        }
    }
}