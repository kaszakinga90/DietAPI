using Application.DTOs.UsersDTO;

namespace Application.FiltersExtensions.UserRoles
{
    public static class UserWithRoleExtension
    {
        public static IQueryable<UserWithRoleGetDTO> Search(this IQueryable<UserWithRoleGetDTO> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return query;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return query.Where(p => p.Email.ToLower().Contains(lowerCaseSearchTerm));
        }
    }
}
