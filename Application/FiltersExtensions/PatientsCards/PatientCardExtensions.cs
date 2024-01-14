using Application.DTOs.PatientCardDTO;

namespace Application.FiltersExtensions.PatientsCards
{
    public static class PatientCardExtensions
    {

        public static IQueryable<PatientCardGetDTO> Search(this IQueryable<PatientCardGetDTO> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return query;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return query.Where(p => p.PatientName.ToLower().Contains(lowerCaseSearchTerm));

        }
    }
}