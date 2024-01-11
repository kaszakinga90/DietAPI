using Application.Core;
using Application.DTOs.FoodCatalogDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.FoodCatalogs
{
    public class FoodCatalogDetails
    {
        public class Query : IRequest<Result<FoodCatalogGetDTO>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<FoodCatalogGetDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<FoodCatalogGetDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var foodCatalog = await _context.FoodCatalogsDb
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                    if (foodCatalog == null)
                    {
                        return Result<FoodCatalogGetDTO>.Failure("Food catalog o podanym id nie został odnaleziony");
                    }

                    return Result<FoodCatalogGetDTO>.Success(_mapper.Map<FoodCatalogGetDTO>(foodCatalog));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<FoodCatalogGetDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}