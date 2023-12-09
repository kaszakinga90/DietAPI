using Application.DTOs.LoginsDTO;
using Application.DTOs.RegistersDTO;
using Application.DTOs.UsersDTO;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenService _tokenService;

        public AccountController(UserManager<User> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                return Unauthorized();

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                IsPatient = user.isPatient,
                IsDietician = user.isDietician,
                IsAdmin = user.isAdmin
            };
        }

        // IMPORTANT : FROM SQL
        [HttpPost("registerpatient")]
        public async Task<ActionResult> RegisterPatient(RegisterDTO registerDTO)
        {
            var user = new Patient
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                isPatient = true,
                isDietician = false,
                isAdmin = false,
                AddressId = null
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }

            return StatusCode(201);
        }

        // IMPORTANT : FROM SQL
        [HttpPost("registerdietician")]
        public async Task<ActionResult> RegisterDietician(RegisterDTO registerDTO)
        {
            var user = new Dietician
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                isPatient = false,
                isDietician = true,
                isAdmin = false,
                AddressId = null
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }

            return StatusCode(201);
        }
        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                IsPatient = user.isPatient,
                IsDietician = user.isDietician,
                IsAdmin = user.isAdmin
            };
        }
    }
}