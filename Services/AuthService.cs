using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jan24ft_bet_ca_kronosGR.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace jan24ft_bet_ca_kronosGR.Services
{
    public class AuthService
    {
        private readonly DataContext _dataContext;
        private readonly JwtSettings _jwtSetting;

        public AuthService(DataContext datacontext, JwtSettings jwtSettings)
        {

            _dataContext = datacontext;
            _jwtSetting = jwtSettings;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _dataContext.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> RegisterUserAsync(string username, string password)
        {
            if (await _dataContext.Users.AnyAsync(u => u.Username == username))
                return false;

            var passwordHasher = new PasswordHasher<User>();
            var user = new User
            {
                Username = username,
                PasswordHash = passwordHasher.HashPassword(null, password)
            };

            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (user == null) return false;

            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }

        public string GenerateToken(User user)
        {
            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSetting.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}