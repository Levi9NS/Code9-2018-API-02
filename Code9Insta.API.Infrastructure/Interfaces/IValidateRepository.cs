using System;

namespace Code9Insta.API.Infrastructure.Interfaces
{
    public interface IValidateRepository
    {
        bool Save();

        bool ValidateLogin(string userName, string password);
        bool IsUserNameHandleUnique(string userName, string handle);
        void SaveRefreshToken(string userName, string refreshToken);
        bool ValidateRefrashToken(Guid userId, string refreshToken);
    }
}
