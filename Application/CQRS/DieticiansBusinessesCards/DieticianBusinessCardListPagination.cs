//using Application.Core;
//using Application.DTOs.DieticianBusinessCardDTO;
//using Application.FiltersExtensions.PatientMessages;
//using DietDB;
//using MediatR;
//using Microsoft.EntityFrameworkCore;

//namespace Application.CQRS.DieticiansBusinessesCards
//{
//    public class DieticianBusinessCardListPagination
//    {
//        public class Query : IRequest<Result<PagedList<DieticianBusinessCardGetDTO>>>
//        {
//            public int PatientId { get; set; }
//            public PatientMessagesParams Params { get; set; }
//        }

//        public class Handler : IRequestHandler<Query, Result<PagedList<DieticianBusinessCardGetDTO>>>
//        {
//            private readonly DietContext _context;

//            public Handler(DietContext context)
//            {
//                _context = context;
//            }

//            public async Task<Result<PagedList<DieticianBusinessCardGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
//            {
//                var dietician = await _context.DieticiansDb
//                     .Where(m => m.PatientId == request.PatientId && m.AdminId == null)
//                    .Select(m => new MessageToDTO
//                    {
//                         .Include(d => d.DieticianOffices)
//                             .ThenInclude(o => o.Office)
//                                 .ThenInclude(a => a.Address)
//                         .Include(d => d.Diplomas)
//                         .Include(d => d.DieticianSpecializations)
//                                 .ThenInclude(s => s.Specialization)
//                         .Include(d => d.Logo);

//                if (dietician == null)
//                {
//                    return Result<DieticianBusinessCardGetDTO>.Failure("Dietician not found.");
//                }

//                var dieticianBusinessCardDTO = _mapper.Map<DieticianBusinessCardGetDTO>(dietician);

//                return Result<DieticianBusinessCardGetDTO>.Success(dieticianBusinessCardDTO);
//            }
//        }
//    }
//}

