using Code9Insta.API.Infrastructure.Entities;
using Code9Insta.API.Infrastructure.Identity;
using System;
using System.Collections.Generic;

namespace Code9Insta.API.Infrastructure.Interfaces
{
    public interface IProfileRepository
    {
        bool Save();

        void CreateProfile(Profile profile);
        Profile GetProfile(Guid userId);
        byte[] GetSaltByUserName(string userName);
        ApplicationUser GetUserInfo(Guid userId);
        Guid? GetUserIdByUserName(string userName);
        Guid? GetProfileIdByUserId(Guid userId);
        Profile GetProfileByHandle(string handle);
        List<Profile> GetAllProfiles();
    }
}
