﻿using Application.Core;
using Application.CQRS.Admins;
using Application.CQRS.Roles;
using Application.CQRS.UserRoles;
using Application.CQRS.Users;
using Application.DTOs.AdminDTO;
using Application.DTOs.RoleDTO;
using Application.DTOs.UsersDTO.UserRoleDTO;
using Application.FiltersExtensions.Admins;
using Application.FiltersExtensions.Roles;
using Application.FiltersExtensions.UserRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        public AdminController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAdmins([FromQuery] AdminParams pagingParams)
        {
            var query = await _mediator.Send(new AdminList.Query { Params = pagingParams });
            return HandlePagedResult(query);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdmin(int id)
        {
            var query = new AdminDetails.Query { AdminId = id };

            return HandleResult(await _mediator.Send(query));
        }

        //metoda tylko dla superadmina
        [HttpPost("createAdmin")]
        public async Task<IActionResult> CreateAdmin(AdminPostDTO AdminPostDTO)
        {
            var result = await _mediator.Send(new AdminCreate.Command { AdminPostDTO = AdminPostDTO });
            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAdmin(int id, [FromForm] AdminEditDTO adminDTO, [FromForm] IFormFile file)
        {
            var command = new AdminEdit.Command
            {
                Admin = adminDTO,
                File = file
            };
            command.Admin.Id = id;

            return HandleResult(await _mediator.Send(command));
        }

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

        [HttpPost("messageToDietician/{adminId}")]
        public async Task<IActionResult> MessageToDietetician(int adminId, MessageToDTO message)
        {
            var command = new MessageToDieteticianFromAdminCreate.Command 
            { 
                MessageDTO = message, 
                AdminId = adminId 
            };
            return HandleResult(await _mediator.Send(command));
        }

        [HttpPost("messageToPatient/{adminId}")]
        public async Task<IActionResult> MessageToPatient(int adminId, MessageToDTO message)
        {
            var command = new MessageToPatientFromAdminCreate.Command
            {
                MessageDTO = message,
                AdminId = adminId
            };
            return HandleResult(await _mediator.Send(command));
        }

        // metoda tylko dla superadmina
        [HttpPost("rolesManage/create")]
        public async Task<IActionResult> CreateRole(RolePostDTO rolePostDTO)
        {
            var command = new RoleCreate.Command
            {
                RolePostDTO = rolePostDTO
            };
            return HandleResult(await _mediator.Send(command));
        }

        // metoda tylko dla superadmina
        [HttpGet("rolesManage/all")]
        public async Task<IActionResult> GetRoles([FromQuery] RoleParams pagingParams)
        {
            var query = await _mediator.Send(new RoleList.Query { Params = pagingParams });
            return HandlePagedResult(query);
        }

        // metoda tylko dla superadmina
        [HttpGet("rolesManage/noPagination/all")]
        public async Task<IActionResult> GetRolesNoPagination()
        {
            var query = await _mediator.Send(new RoleNoPaginationList.Query());
            return HandleResult(query);
        }

        // metoda tylko dla superadmina
        [HttpGet("rolesManage/users/all")]
        public async Task<IActionResult> GetUsers([FromQuery] UserWithRoleParams pagingParams)
        {
            var query = await _mediator.Send(new UserRoleList.Query { Params = pagingParams });
            return HandlePagedResult(query);
        }

        // metoda tylko dla superadmina
        [HttpGet("rolesManage/userRoles/{userId}")]
        public async Task<IActionResult> GetRolesForUser(int userId)
        {
            var query = await _mediator.Send(new UserDetails.Query { UserId = userId } );
            return HandleResult(query);
        }
        
        // metoda tylko dla superadmina
        [HttpDelete("rolesManage/userRoles/deleteRole/{userId}/{userRoleId}")]
        public async Task<IActionResult> RemoveUserRole(int userId, int userRoleId)
        {
            var query = await _mediator.Send(new UserRoleDelete.Command { UserId = userId, UserRoleId = userRoleId });
            return HandleResult(query);
        }

        // metoda tylko dla superadmina
        [HttpPost("rolesManage/userRoles/addRole")]
        public async Task<IActionResult> AddUserRole(UserRoleCreateDTO userRoleCreateDTO)
        {
            var query = await _mediator.Send(new UserRoleCreate.Command { UserRoleCreateDTO = userRoleCreateDTO });
            return HandleResult(query);
        }
    }
}