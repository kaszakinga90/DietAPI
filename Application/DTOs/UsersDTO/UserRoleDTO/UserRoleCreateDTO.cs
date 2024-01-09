namespace Application.DTOs.UsersDTO.UserRoleDTO
{
    public class UserRoleCreateDTO
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
    }
}
