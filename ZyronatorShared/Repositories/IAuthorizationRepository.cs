using System;
using ZyronatorShared.TokenAuthorization;

namespace ZyronatorShared.Repositories
{
    public interface IAuthorizationRepository
    {
        Token Authorize(string userName, string userPassword);
        Token Authorize(Guid token);
    }
}
