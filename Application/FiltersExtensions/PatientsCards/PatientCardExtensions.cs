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

        public static IQueryable<PatientCardGetDTO> PatientCardSort(this IQueryable<PatientCardGetDTO> query, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(d => d.PatientName);
            query = orderBy switch
            {
                "patientNameAsc" => query.OrderBy(d => d.PatientName),
                "patientNameDesc" => query.OrderByDescending(d => d.PatientName),
                _ => query.OrderBy(d => d.PatientName)
            };
            return query;
        }

        public static IQueryable<PatientCardGetDTO> PatientCardFilter(this IQueryable<PatientCardGetDTO> query, string patientName)
        {
            var patientNameList = new List<string>();
            if (!string.IsNullOrEmpty(patientName))
                patientNameList.AddRange(patientName.ToLower().Split(",").ToList());

            query = query.Where(m => patientNameList.Count == 0 || patientNameList.Contains(m.PatientName.ToLower()));

            return query;
        }
    }
}