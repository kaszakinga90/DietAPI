using Application.Core;
using Application.DTOs.PrintoutsDTO;
using DietDB;
using MediatR;
using Xceed.Words.NET;

namespace Application.CQRS.Printouts
{
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

            public async Task<Result<byte[]>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.Data.DieticianId);
                if (user == null)
                {
                    return Result<byte[]>.Failure("Użytkownik nie został znaleziony.");
                }

                using (var memoryStream = new MemoryStream())
                {
                    await request.Data.TemplateFile.CopyToAsync(memoryStream);
                    using (var doc = DocX.Load(memoryStream))
                    {
                        // Tutaj możesz zastąpić placeholdery danymi użytkownika
                        doc.ReplaceText("{FirstName}", user.FirstName ?? string.Empty);
                        doc.ReplaceText("{LastName}", user.LastName ?? string.Empty);

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
