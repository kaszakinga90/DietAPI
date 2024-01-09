using Application.Core;
using Application.DTOs.DishDTO;
using AutoMapper;
using DietDB;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.Dishes
{
    public class DishDetails
    {
        public class Query : IRequest<Result<DishGetDTO>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DishGetDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<DishGetDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dish = await _context.DishesDb
                    .FindAsync(request.Id, cancellationToken);

                    if (dish == null)
                    {
                        return Result<DishGetDTO>.Failure("no results");
                    }

                    return Result<DishGetDTO>.Success(_mapper.Map<DishGetDTO>(dish));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<DishGetDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}