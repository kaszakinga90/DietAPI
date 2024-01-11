using Application.Core;
using Application.DTOs.PrintoutsDTO;
using DietDB;
using MediatR;
using Xceed.Words.NET;

namespace Application.CQRS.Printouts
{
    public class PrintoutDocumentCreate : IRequest<Result<string>>
    {
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
                // Znajdź szablon w bazie danych
                var printoutTemplate = await _context.PrintoutsDb
                    .FindAsync(request.Data.Id);

                var user = await _context.Users.FindAsync(request.Data.DieticianId); // Przykład, zakładam, że UserId jest częścią DTO

                if (printoutTemplate == null || user == null)
                {
                    return Result<string>.Failure("Szablon lub użytkownik nie został znaleziony.");
                }

                // Generuj dokument
                string filePath = GenerateWordDocument(printoutTemplate.Data, user.FirstName, user.LastName);

                return Result<string>.Success(filePath);
            }

            private string GenerateWordDocument(string templateData, string firstName, string lastName)
            {
                var doc = DocX.Create("output.docx");

                // Zastąpienie znaczników w szablonie odpowiednimi wartościami
                templateData = templateData.Replace("{FirstName}", firstName ?? string.Empty);
                templateData = templateData.Replace("{LastName}", lastName ?? string.Empty);

                doc.InsertParagraph(templateData);

                string filePath = Path.Combine("test", "output.docx");
                doc.SaveAs(filePath);

                return filePath;
            }

            //private string GenerateWordDocument(string templateData, string data)
            //{
            //    var doc = DocX.Create("output.docx");

            //    // Tutaj możesz dodać logikę manipulowania zawartością dokumentu na podstawie templateData i data
            //    doc.InsertParagraph(templateData);
            //    doc.InsertParagraph(data);

            //    string filePath = Path.Combine("test", "output.docx");
            //    doc.SaveAs(filePath);

            //    return filePath;
            //}
        }

    }
}