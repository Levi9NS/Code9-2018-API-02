using Code9Insta.API.Infrastructure.Data;
using Code9Insta.API.Infrastructure.Entities;
using Code9Insta.API.Infrastructure.Identity;
using Code9Insta.API.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Code9Insta.API.Infrastructure.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly CodeNineDbContext _context;

        public ProfileRepository(CodeNineDbContext context)
        {
            _context = context;
        }

        public void CreateProfile(Profile profile)
        {
            _context.Profiles.Add(profile);           
        }

        public Profile GetProfile(Guid userId)
        {
           return _context.Profiles.Include(i => i.User).FirstOrDefault(f => f.UserId == userId);
        }

        public byte[] GetSaltByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(f => f.UserName == userName).Salt;
        }

        public ApplicationUser GetUserInfo(Guid userId)
        {
            return _context.Users.FirstOrDefault(f => f.Id == userId);
        }

        public Guid? GetUserIdByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(f => f.UserName == userName)?.Id;
        }

        public Guid? GetProfileIdByUserId(Guid userId)
        {
            return _context.Profiles.FirstOrDefault(f => f.UserId == userId)?.Id;
        }

        public Profile GetProfileByHandle(string handle)
        {
            return _context.Profiles.Include(i => i.User).FirstOrDefault(f => f.Handle == handle);
        }

        public List<Profile> GetAllProfiles()
        {
            return _context.Profiles.Include(i => i.User).ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
