using Application.DTOs.DieticianBusinessCardDTO;

namespace Application.FiltersExtensions.DieticianBussinesCards
{
    public static class DieticianBussinesCardsExtensions
    {
        public static IQueryable<DieticianBusinessCardGetDTO> BusinessCardSort(this IQueryable<DieticianBusinessCardGetDTO> query, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(d => d.DieticianName);
            query = orderBy switch
            {
                "name" => query.OrderBy(d => d.DieticianName),
                "officeName" => query.OrderBy(d => d.DieticianOffices.FirstOrDefault().OfficeName),
                "specialization" => query.OrderBy(d => d.DieticianSpecializations.FirstOrDefault().SpecializationName),
                "countryState" => query.OrderBy(d => d.DieticianOffices.FirstOrDefault().AddressDTO.StateName),
                _ => query.OrderBy(d => d.DieticianName)
            };
            return query;
        }

        //public static IQueryable<DieticianBusinessCardGetDTO> BusinessCardSearch(this IQueryable<DieticianBusinessCardGetDTO> query, string searchTerm)
        //{
        //    if (string.IsNullOrWhiteSpace(searchTerm)) return query;
        //    var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
        //    return query.Where(d => d.DieticianName.ToLower().Contains(lowerCaseSearchTerm) ||
        //                            d.DieticianSpecializations.FirstOrDefault().SpecializationName.ToLower().Contains(lowerCaseSearchTerm)
        //                      );
        //}
        public static IQueryable<DieticianBusinessCardGetDTO> BusinessCardSearch(
    this IQueryable<DieticianBusinessCardGetDTO> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return query;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return query.Where(d =>
                d.DieticianName.ToLower().Contains(lowerCaseSearchTerm) ||
                d.DieticianSpecializations.Any(s =>
                    s.SpecializationName.ToLower().Contains(lowerCaseSearchTerm))
            );
        }


        public static IQueryable<DieticianBusinessCardGetDTO> BusinessCardFilter(
             this IQueryable<DieticianBusinessCardGetDTO> query,
             string specializationNames,
             string stateNames)
                 {
                     var specializationNameList = !string.IsNullOrEmpty(specializationNames)
                         ? specializationNames.ToLower().Split(",").ToList()
                         : new List<string>();

                     return query.Where(d =>
                         (!specializationNameList.Any() ||
                          d.DieticianSpecializations.Any(s =>
                              specializationNameList.Contains(s.SpecializationName.ToLower())))
                         &&
                         (string.IsNullOrEmpty(stateNames) ||
                          d.DieticianOffices.Any(o =>
                              o.AddressDTO.StateName.ToLower() == stateNames.ToLower())));
                 }


        //public static IQueryable<DieticianBusinessCardGetDTO> BusinessCardFilter(this IQueryable<DieticianBusinessCardGetDTO> query, string specializationNames, string stateNames)
        //{
        //    var specializationNameList = new List<string>();
        //    if (!string.IsNullOrEmpty(specializationNames))
        //        specializationNameList.AddRange(specializationNames.ToLower().Split(",").ToList());

        //    query = query.Where(d =>
        //        specializationNameList.Count == 0 || d.DieticianSpecializations.Any(s => specializationNameList.Contains(s.SpecializationName.ToLower()))
        //        || (stateNames == null || d.DieticianOffices.Any(o => o.AddressDTO.StateName == stateNames)));
        //    return query;
        //}

        //public static IQueryable<DieticianBusinessCardGetDTO> BusinessCardFilter(this IQueryable<DieticianBusinessCardGetDTO> query, string specializationNames)
        //{
        //    var specializationNameList = new List<string>();
        //    if (!string.IsNullOrEmpty(specializationNames))
        //        specializationNameList.AddRange(specializationNames.ToLower().Split(",").ToList());

        //    query = query.Where(d =>
        //        specializationNameList.Count == 0 || d.DieticianSpecializations.Any(s => specializationNameList.Contains(s.SpecializationName.ToLower()))
        //        );
        //    return query;
        //}
    }
}