namespace Application.FiltersExtensions.Patients
{
    public static class PatientExtensions
    {
        public static IQueryable<PatientGetDTO> PatientSort(this IQueryable<PatientGetDTO> query, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(d => d.PatientName);
            query = orderBy switch
            {
                "patientNameAsc" => query.OrderBy(d => d.PatientName),
                "patientNameDesc" => query.OrderByDescending(d => d.PatientName),
                "email" => query.OrderBy(d => d.Email),
                _ => query.OrderBy(d => d.PatientName)
            };
            return query;
        }

        public static IQueryable<PatientGetDTO> PatientSearch(this IQueryable<PatientGetDTO> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return query;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return query.Where(p => p.PatientName.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<PatientGetDTO> PatientFilter(this IQueryable<PatientGetDTO> query, string patientName)
        {
            var patientNameList = new List<string>();
            if (!string.IsNullOrEmpty(patientName))
                patientNameList.AddRange(patientName.ToLower().Split(",").ToList());

            query = query.Where(m => patientNameList.Count == 0 || patientNameList.Contains(m.PatientName.ToLower()));

            return query;
        }
    }
}