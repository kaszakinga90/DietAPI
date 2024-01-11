using Application.Core;
using Application.DTOs.PrintoutsDTO;
using DietDB;
using MediatR;
using Microsoft.AspNetCore.Http;
using ModelsDB.Functionality;
using System.Diagnostics;
using Xceed.Words.NET;

namespace Application.CQRS.Printouts
{
    public class PrintoutTemplateCreate : IRequest<Result<ParameterizedPrintoutPostDTO>>
    {
        public class Command : IRequest<Result<ParameterizedPrintoutPostDTO>>
        {
            public ParameterizedPrintoutPostDTO Data { get; set; }
        }
        
        public class Handler : IRequestHandler<Command, Result<ParameterizedPrintoutPostDTO>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<ParameterizedPrintoutPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                string fileContent = ConvertWordFileToText(request.Data.WordFile);

                var printout = new Printout
                {
                    Name = request.Data.Name,
                    Description = request.Data.Description,
                    Data = fileContent,
                };

                _context.PrintoutsDb.Add(printout);

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result)
                    {
                        return Result<ParameterizedPrintoutPostDTO>.Failure("Operacja nie powiodła się.");
                    }

                    return Result<ParameterizedPrintoutPostDTO>.Success(request.Data);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<ParameterizedPrintoutPostDTO>.Failure("Wystąpił błąd podczas dodawania szablonu.");
                }
            }

            private string ConvertWordFileToText(IFormFile file)
            {
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    using (var document = DocX.Load(stream))
                    {
                        return document.Text;
                    }
                }
            }
        }
    }
}
