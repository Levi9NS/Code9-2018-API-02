using Code9Insta.API.Infrastructure.Data;
using Code9Insta.API.Infrastructure.Interfaces;
using System;
using System.Linq;

namespace Code9Insta.API.Infrastructure.Repository
{
    public class ValidateRepository : IValidateRepository
    {
        private readonly CodeNineDbContext _context;

        public ValidateRepository(CodeNineDbContext context)
        {
            _context = context;
        }

        public bool ValidateLogin(string userName, string password)
        {
            return _context.Users.Any(a => a.UserName == userName && a.PasswordHash == password);
        }

        public bool IsUserNameHandleUnique(string userName, string handle)
        {
            return _context.Users.All(a => a.UserName != userName) && _context.Profiles.All(a => a.Handle != handle);
        }

        public void SaveRefreshToken(string userName, string refreshToken)
        {
            var userToUpdate = _context.Users.SingleOrDefault(s => s.UserName == userName);
            userToUpdate.RefreshToken = refreshToken;

            _context.Users.Update(userToUpdate);

        }

        public bool ValidateRefrashToken(Guid userId, string refreshToken)
        {
            return _context.Users.Any(a => a.RefreshToken == refreshToken && a.Id == userId);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
