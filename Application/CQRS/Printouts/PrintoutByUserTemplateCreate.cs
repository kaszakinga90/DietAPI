using Application.Core;
using Application.DTOs.PrintoutsDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xceed.Words.NET;

namespace Application.CQRS.Printouts
{
    /// <summary>
    /// Klasa służąca do tworzenia pliku wydruku parametryzowanego z dokumentu, który dostarczył użytkownik
    /// </summary>
    public class PrintoutByUserTemplateCreate : IRequest<Result<byte[]>>
    {
        public class Command : IRequest<Result<byte[]>>
        {
            public PrintoutUploadByUserPostDTO Data { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<byte[]>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Obsługa żądania utworzenia pliku
            /// </summary>
            public async Task<Result<byte[]>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.DieticiansDb
                    .Include(u => u.Address)
                    .FirstOrDefaultAsync(u => u.Id == request.Data.DieticianId);

                if (user == null)
                {
                    return Result<byte[]>.Failure("Użytkownik nie został znaleziony.");
                }

                // Wykorzystanie strumienia pamięci 
                using (var memoryStream = new MemoryStream())
                {
                    // Kopiowanie zawartości dokumentu do strumienia pamięci
                    await request.Data.TemplateFile.CopyToAsync(memoryStream);

                    // Załadowanie pliku z pamięci
                    using (var doc = DocX.Load(memoryStream))
                    {
                        // Zamiana znaczników na informacje
                        doc.ReplaceText("{FirstName}", user.FirstName ?? string.Empty);
                        doc.ReplaceText("{LastName}", user.LastName ?? string.Empty);
                        doc.ReplaceText("{Email}", user.Email ?? string.Empty);
                        doc.ReplaceText("{PhoneNumber}", user.PhoneNumber ?? string.Empty);
                        doc.ReplaceText("{City}", user.Address.City ?? string.Empty);
                        doc.ReplaceText("{Street}", user.Address.Street ?? string.Empty);
                        doc.ReplaceText("{LocalNo}", user.Address.LocalNo ?? string.Empty);
                        doc.ReplaceText("{ZipCode}", user.Address.ZipCode ?? string.Empty);
                        doc.ReplaceText("{Country}", user.Address.Country ?? string.Empty);

                        // Zapisanie przekształconego pliku do nowego strumienia
                        using (var output = new MemoryStream())
                        {
                            doc.SaveAs(output);
                            return Result<byte[]>.Success(output.ToArray());
                        }
                    }
                }
            }
        }
    }
}