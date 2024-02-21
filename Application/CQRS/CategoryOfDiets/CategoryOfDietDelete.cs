using Application.Core;
using Application.DTOs.CategoryOfDietDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.CategoryOfDiets
{
    public class CategoryOfDietDelete
    {
        public class Command : IRequest<Result<CategoryOfDietDeleteDTO>>
        {
            public int Id { get; set; }
            public CategoryOfDietDeleteDTO CategoryOfDietDeleteDTO { get; set; }

        }
        public class Handler : IRequestHandler<Command, Result<CategoryOfDietDeleteDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<CategoryOfDietDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var categoryOfDiet = await _context.CategoryOfDietsDb.FirstOrDefaultAsync(i => i.Id == request.Id);

                if (categoryOfDiet == null)
                {
                    return Result<CategoryOfDietDeleteDTO>.Failure("kategoria diety o podanym ID nie została znaleziona.");
                }

                _mapper.Map(request.CategoryOfDietDeleteDTO, categoryOfDiet);

                categoryOfDiet.isActive = false;

                try
                {
                    var result = await _context.SaveChangesAsync() > 0;
                    if (!result)
                    {
                        return Result<CategoryOfDietDeleteDTO>.Failure("Usunięcie kategorii nie powiodło się.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenie: " + ex);
                    return Result<CategoryOfDietDeleteDTO>.Failure("Wystąpił błąd podczas usuwania kategorii. " + ex);
                }

                return Result<CategoryOfDietDeleteDTO>.Success(_mapper.Map<CategoryOfDietDeleteDTO>(categoryOfDiet));
            }
        }
    }
}
