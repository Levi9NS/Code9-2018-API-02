
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Code9Insta.API.Helpers.Interfaces
{
    public interface IAuthorizationManager
    {
        string GeneratePasswordHash(string password, byte[] salt);
        JwtSecurityToken GenerateToken(SymmetricSecurityKey key, string userName, Guid? userId);
    }
}
