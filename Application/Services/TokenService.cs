using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelsDB;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    /// <summary>
    /// Serwis do generowania tokenów JWT dla użytkowników
    /// </summary>
    public class TokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        // iNICJALIZACJA SERWISU z UserManager i konfiguracją
        public TokenService(UserManager<User> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        /// <summary>
        /// Generuje token JWT dla określonego użytkownika.
        /// </summary>
        /// <param name="user">Użytkownik, dla którego ma być wygenerowany token.</param>
        /// <returns>Token JWT jako ciąg znaków.</returns>
        public async Task<string> GenerateToken(User user)
        {
            // Tworzenie listy elementów, ktore są brane pod uwagę przy sprawdzaniu zalogowanej osoby
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            // Przypisywanie ról użytkownika
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Generowanie klucza do podpisania tokena.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:TokenKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            // Tworzenie tokena JWT z określonymi opcjami, kluczem i czasem wygaśnięcia
            var tokenOptions = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            // Zwracanie tokena JWT jako ciągu znaków
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}