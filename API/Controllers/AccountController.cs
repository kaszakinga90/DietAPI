using Application.DTOs.LoginsDTO;
using Application.DTOs.RegistersDTO;
using Application.DTOs.UsersDTO;
using Application.Services;
using Application.Services.EmailSends;
using MediatR;
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
        private readonly IEmailSender _emailService;

        public AccountController(UserManager<User> userManager, TokenService tokenService, IEmailSender emailService, IMediator mediator) : base(mediator)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                return Unauthorized();

            if (!user.EmailConfirmed)
                return Unauthorized("Email nie został potwierdzony.");

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
        [HttpPost("registerPatient")]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterPatient(RegisterDTO registerDTO)
        {
            var user = new Patient
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                isPatient = true,
                isDietician = false,
                isAdmin = false,
                isActive = false,
                AddressId = null
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if(result.Succeeded)
            {
                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                if (emailConfirmationToken != null)
                {
                    var confirmationLink = $"http://localhost:3000/registerConfirm?userId={user.Id}&token={emailConfirmationToken}";
                    var emailBody = $"Potwierdź swoje konto, klikając <a href='{confirmationLink}'>tutaj</a>";
                    var message = new EmailMessage(new string[] { "testtesttest@test.com" }, "Test email", emailBody);
                    _emailService.SendEmail(message);
                }
            }

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
        [HttpPost("registerDietician")]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
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

        // IMPORTANT : FROM SQL
        [HttpPost("registerAdmin")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAdmin(RegisterDTO registerDTO)
        {
            var user = new Admin
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                isPatient = false,
                isDietician = false,
                isAdmin = true,
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

        [AllowAnonymous]
        [HttpPost("registerConfirm")]
        public async Task<ActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest("Invalid parameters for email confirmation.");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest("User not found for email confirmation.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            Console.WriteLine($"ConfirmEmail result: {result.Succeeded}");

            if (result.Succeeded)
            {
                user.isActive = true;
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);

                return Ok("Email confirmation successful.");
            }
            else
            {
                Console.WriteLine($"ConfirmEmail failed. Errors: {string.Join(", ", result.Errors)}");
                return BadRequest("Email confirmation failed.");
            }
        }

    }
}