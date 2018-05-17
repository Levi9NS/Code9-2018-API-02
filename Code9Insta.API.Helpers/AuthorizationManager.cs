using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Code9Insta.API.Helpers.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;

namespace Code9Insta.API.Helpers
{
    public class AuthorizationManager : IAuthorizationManager
    {
        public string GeneratePasswordHash(string password, byte[] salt)
        {
            var hashed = string.Empty;

                // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
               password: password,
               salt: salt,
               prf: KeyDerivationPrf.HMACSHA1,
               iterationCount: 10000,
               numBytesRequested: 256 / 8));
            

            return hashed;
        }

        public JwtSecurityToken GenerateToken(SymmetricSecurityKey key, string userName, Guid? userId)
        {
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "code9.com",
                audience: "code9.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return token;
           
        }
    }
}
