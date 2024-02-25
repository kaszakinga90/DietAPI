using Application.Core;
using Application.DTOs.PrintoutsDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

                var user = await _context.DieticiansDb
                    .Include(u => u.Address)
                    .FirstOrDefaultAsync(u => u.Id == incomingData.DieticianId);

                if (printoutTemplate == null || user == null)
                {
                    return Result<byte[]>.Failure("Szablon lub użytkownik nie został znaleziony.");
                }

                // Wygenerowanie pliku word z danymi
                byte[] fileBytes = GenerateWordDocument(printoutTemplate.Data, user.FirstName, user.LastName, user.Email, user.PhoneNumber,
                    user.Address.City, user.Address.Street, user.Address.LocalNo, user.Address.ZipCode, user.Address.Country);

                return Result<byte[]>.Success(fileBytes);
            }
            /// <summary>
            /// Metoda z logiką generowania dokumentu i jego zawartości
            /// </summary>
            private byte[] GenerateWordDocument(string templateData, string firstName, string lastName, string email, string phoneNumber, string city,
                string street, string localNo, string zipCode, string country)
            {
                // Utworzenie nowego dokumentu
                var doc = DocX.Create("output.docx");

                // Zamiana danych na te przekazane
                templateData = templateData.Replace("{FirstName}", firstName ?? string.Empty);
                templateData = templateData.Replace("{LastName}", lastName ?? string.Empty);
                templateData = templateData.Replace("{Email}", email ?? string.Empty);
                templateData = templateData.Replace("{PhoneNumber}", phoneNumber ?? string.Empty);
                templateData = templateData.Replace("{City}", city ?? string.Empty);
                templateData = templateData.Replace("{Street}", street ?? string.Empty);
                templateData = templateData.Replace("{LocalNo}", localNo ?? string.Empty);
                templateData = templateData.Replace("{ZipCode}", zipCode ?? string.Empty);
                templateData = templateData.Replace("{Country}", country ?? string.Empty);

                // Wstawienie sformatowanego szablonu do dokumentu
                doc.InsertParagraph(templateData);

                // Zapisanie pliku jako tablicy bajtów
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    doc.SaveAs(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}