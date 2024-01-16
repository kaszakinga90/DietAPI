using Application.Core;
using Application.FiltersExtensions.DieticianMessages;
using DietDB;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.Dieticians
{
    public class DieticianMessageList
    {
        public class Query : IRequest<Result<PagedList<MessageToDTO>>>
        {
            public int DieticianId { get; set; }
            public DieticianMessagesParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<MessageToDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PagedList<MessageToDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dieticianMessagesList = _context.MessageToDb
                    .Where(m => m.DieticianId == request.DieticianId && m.AdminId == null)
                    .Select(m => new MessageToDTO
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Description = m.Description,
                        DieticianId = m.DieticianId,
                        PatientId = m.PatientId,
                        DieticianName = m.Dietician.FirstName + " " + m.Dietician.LastName,
                        AdminName = m.Admin.FirstName + " " + m.Admin.LastName,
                        PatientName = m.Patient.FirstName + " " + m.Patient.LastName,
                        dateAdded = m.dateAdded,
                        ReadDate = m.ReadDate,
                        IsRead = m.IsRead
                    })
                    .AsQueryable();

                    dieticianMessagesList = dieticianMessagesList.PatientSort(request.Params.OrderBy);
                    dieticianMessagesList = dieticianMessagesList.PatientFilter(request.Params.PatientNames);
                    dieticianMessagesList = dieticianMessagesList.PatientSearch(request.Params.SearchTerm);

                    return Result<PagedList<MessageToDTO>>.Success(
                        await PagedList<MessageToDTO>.CreateAsync(dieticianMessagesList, request.Params.PageNumber, request.Params.PageSize)
                        );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<PagedList<MessageToDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}