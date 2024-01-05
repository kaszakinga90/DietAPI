namespace Application.FiltersExtensions.Diets
{
    public static class DietExtensions
    {
        public static IQueryable<DietGetDTO> DietSort(this IQueryable<DietGetDTO> query, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(d => d.PatientName);
            query = orderBy switch
            {
                "dieticianName" => query.OrderBy(d => d.DieteticanName),
                "patientName" => query.OrderBy(d => d.PatientName),
                "startDate" => query.OrderByDescending(d => d.StartDate),
                _ => query.OrderBy(d => d.PatientName)
            };
            return query;
        }

        public static IQueryable<DietGetDTO> DietSortByDietician(this IQueryable<DietGetDTO> query, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(d => d.DieteticanName);
            query = orderBy switch
            {
                "dieticianName" => query.OrderBy(d => d.DieteticanName),
                "startDate" => query.OrderByDescending(d => d.StartDate),
                _ => query.OrderBy(d => d.DieteticanName)
            };
            return query;
        }

        public static IQueryable<DietGetDTO> DietSortByDieticianToPatientCard(this IQueryable<DietGetDTO> query, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(d => d.Name);
            query = orderBy switch
            {
                "dieticianName" => query.OrderBy(d => d.DieteticanName),
                "startDate" => query.OrderByDescending(d => d.StartDate),
                _ => query.OrderBy(d => d.Name)
            };
            return query;
        }

        public static IQueryable<DietGetDTO> DietSearch(this IQueryable<DietGetDTO> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return query;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return query.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<DietGetDTO> DietFilter(this IQueryable<DietGetDTO> query, string patientName)
        {
            var patientNameList = new List<string>();
            if (!string.IsNullOrEmpty(patientName))
                patientNameList.AddRange(patientName.ToLower().Split(",").ToList());

            query = query.Where(m => patientNameList.Count == 0 || patientNameList.Contains(m.PatientName.ToLower()));

            return query;
        }

        public static IQueryable<DietGetDTO> DietFilterDietician(this IQueryable<DietGetDTO> query, string dieticianName)
        {
            var dieticianNameList = new List<string>();
            if (!string.IsNullOrEmpty(dieticianName))
                dieticianNameList.AddRange(dieticianName.ToLower().Split(",").ToList());

            query = query.Where(m => dieticianNameList.Count == 0 || dieticianNameList.Contains(m.DieteticanName.ToLower()));

            return query;
        }
    }
}
