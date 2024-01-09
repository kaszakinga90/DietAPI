using Application.DTOs.RoleDTO;

namespace Application.DTOs.UsersDTO.UserRoleDTO
{
    public class UserRoleGetDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<RoleGetDTO> UserRolesDTO { get; set; }
    }
}
