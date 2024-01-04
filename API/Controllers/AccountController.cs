using Application.CQRS.Admins;
using Application.CQRS.Dieticians;
using Application.CQRS.Patients;
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
                return Unauthorized("user jest null lub hasło się nie zgadza");

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

        // IMPORTANT : FROM SQL - trigger tworzący obiekt Adres 
        [AllowAnonymous]
        [HttpPost("registerPatient")]
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
                isDarkMode = false,
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

        // IMPORTANT : FROM SQL - trigger tworzący obiekt Adres
        [AllowAnonymous]
        [HttpPost("registerDietician")]
        public async Task<ActionResult> RegisterDietician(RegisterDTO registerDTO)
        {
            var user = new Dietician
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                isPatient = false,
                isDietician = true,
                isAdmin = false,
                isDarkMode=false,
                AddressId = null
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
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

        // IMPORTANT : FROM SQL - trigger tworzący obiekt Adres
        [AllowAnonymous]
        [HttpPost("registerAdmin")]
        public async Task<ActionResult> RegisterAdmin(RegisterDTO registerDTO)
        {
            var user = new Admin
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                isPatient = false,
                isDietician = false,
                isAdmin = true,
                isDarkMode=false,
                AddressId = null
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
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

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            // Nazwa użytkownika jest przekazywana przez token JWT
            var userName = User.Identity.Name;

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound("Użytkownik nie znaleziony.");
            }

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

        [HttpDelete("deleteDietician/{id}")]
        public async Task<IActionResult> DeleteDietician(int id)
        {
            var command = new DieticianDelete.Command { Id = id };
            return HandleResult(await _mediator.Send(command));
        }

        [HttpDelete("deletePatient/{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var command = new PatientDelete.Command { Id = id };
            return HandleResult(await _mediator.Send(command));
        }

        [HttpDelete("deleteAdmin/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var command = new AdminDelete.Command { Id = id };
            return HandleResult(await _mediator.Send(command));
        }
    }
}