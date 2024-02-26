using Application.Core;
using Application.CQRS.Admins;
using Application.CQRS.Messages;
using Application.CQRS.Roles;
using Application.CQRS.UserRoles;
using Application.CQRS.Users;
using Application.DTOs.AdminDTO;
using Application.DTOs.MessagesDTO;
using Application.DTOs.RoleDTO;
using Application.DTOs.UsersDTO.UserRoleDTO;
using Application.FiltersExtensions.Admins;
using Application.FiltersExtensions.Roles;
using Application.FiltersExtensions.UserRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        public AdminController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAdmins([FromQuery] AdminParams pagingParams)
        {
            var query = await _mediator.Send(new AdminList.Query { Params = pagingParams });
            return HandlePagedResult(query);
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdmin(int id)
        {
            var query = new AdminDetails.Query { AdminId = id };

            return HandleResult(await _mediator.Send(query));
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("createAdmin")]
        public async Task<IActionResult> CreateAdmin(AdminPostDTO AdminPostDTO)
        {
            var result = await _mediator.Send(new AdminCreate.Command { AdminPostDTO = AdminPostDTO });

            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Admin został dodany pomyślnie." });
            }
            return BadRequest(result.Error);
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAdmin(int id, [FromForm] AdminEditDTO adminDTO, [FromForm] IFormFile file)
        {
            var command = new AdminEdit.Command
            {
                Admin = adminDTO,
                File = file
            };
            command.Admin.Id = id;

            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie zedytowano admina." });
            }
            return BadRequest(result.Error);
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet("messages/{adminId}")]
        public async Task<ActionResult<PagedList<MessageToDTO>>> GetMessagesForAdmin(int adminId, [FromQuery] PagingParams param)
        {
            var result = await _mediator.Send(new AdminMessageList.Query 
            { 
                AdminId = adminId, 
                Params = param 
            });
            return HandlePagedResult(result);
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost("messageToDietician/{adminId}")]
        public async Task<IActionResult> MessageToDietetician(int adminId, MessageToDTO message)
        {
            var command = new MessageToDieteticianFromAdminCreate.Command 
            { 
                MessageDTO = message, 
                AdminId = adminId 
            };

            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Wiadomość została wysłana." });
            }
            return BadRequest(result.Error);
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost("messageToPatient/{adminId}")]
        public async Task<IActionResult> MessageToPatient(int adminId, MessageToDTO message)
        {
            var command = new MessageToPatientFromAdminCreate.Command
            {
                MessageDTO = message,
                AdminId = adminId
            };

            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Wiadomość została wysłana." });
            }
            return BadRequest(result.Error);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("rolesManage/create")]
        public async Task<IActionResult> CreateRole(RolePostDTO rolePostDTO)
        {
            var command = new RoleCreate.Command { RolePostDTO = rolePostDTO };
            var result = await _mediator.Send(command);

            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Rola została utworzona." });
            }
            return BadRequest(result.Error);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("rolesManage/all")]
        public async Task<IActionResult> GetRoles([FromQuery] RoleParams pagingParams)
        {
            var query = await _mediator.Send(new RoleList.Query { Params = pagingParams });
            return HandlePagedResult(query);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("rolesManage/noPagination/all")]
        public async Task<IActionResult> GetRolesNoPagination()
        {
            var query = await _mediator.Send(new RoleNoPaginationList.Query());
            return HandleResult(query);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("rolesManage/users/all")]
        public async Task<IActionResult> GetUsers([FromQuery] UserWithRoleParams pagingParams)
        {
            var query = await _mediator.Send(new UserRoleList.Query { Params = pagingParams });
            return HandlePagedResult(query);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("rolesManage/userRoles/{userId}")]
        public async Task<IActionResult> GetRolesForUser(int userId)
        {
            var query = await _mediator.Send(new UserDetails.Query { UserId = userId } );
            return HandleResult(query);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("rolesManage/userRoles/deleteRole/{userId}/{userRoleId}")]
        public async Task<IActionResult> RemoveUserRole(int userId, int userRoleId)
        {
            var command = new UserRoleDelete.Command { UserId = userId, UserRoleId = userRoleId };
            var result = await _mediator.Send(command);

            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Rola została usunięta." });
            }
            return BadRequest(result.Error);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("rolesManage/userRoles/addRole")]
        public async Task<IActionResult> AddUserRole(UserRoleCreateDTO userRoleCreateDTO)
        {
            var command = new UserRoleCreate.Command { UserRoleCreateDTO = userRoleCreateDTO };
            var result = await _mediator.Send(command);

            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Rola została dodana." });
            }
            return BadRequest(result.Error);
        }

        [Authorize(Roles = "SuperAdmin, Admin, Dietetician, Patient")]
        [HttpGet("allnopagination")]
        public async Task<IActionResult> GetAdminsListNoPagination()
        {
            var result = await _mediator.Send(new AdminNoPaginationList.Query());
            return HandleResult(result);
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost("message/isread")]
        public async Task<IActionResult> MessageIsRead(MessageIsReadPostDTO messageIsRead)
        {
            var command = new MessageIsRead.Command
            {
                MessageIsReadPostDTO = messageIsRead
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}