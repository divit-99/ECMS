namespace ECMS.API.Services
{
    using ECMS.API.DTOs.User;
    using ECMS.API.Models;
    using ECMS.API.Repositories.Interfaces;
    using ECMS.API.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;
    using System.ComponentModel.DataAnnotations;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _config;
        private readonly PasswordHasher<User> _hasher = new();

        public AuthService(IUserRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task<bool> SignupAsync(SignupDto dto)
        {
            var existing = await _repo.GetByEmailAsync(dto.Email);
            if (existing != null)
                throw new ValidationException("Email already exists!");

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                IsAdmin = dto.IsAdmin
            };

            user.PasswordHash = _hasher.HashPassword(user, dto.Password);

            await _repo.AddAsync(user);

            return true;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _repo.GetByEmailAsync(dto.Email);
            if (user == null)
                throw new ValidationException("Invalid email or password!");

            var check = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (check == PasswordVerificationResult.Failed)
                throw new ValidationException("Invalid email or password!");

            return new AuthResponseDto
            {
                Token = GenerateJwt(user),
                FullName = user.FullName,
                IsAdmin = user.IsAdmin
            };
        }

        private string GenerateJwt(User user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim("fullName", user.FullName),
            new Claim("email", user.Email),
            new Claim("isAdmin", user.IsAdmin.ToString())
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
