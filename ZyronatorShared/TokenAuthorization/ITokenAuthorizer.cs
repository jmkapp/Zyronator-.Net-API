using System;

namespace ZyronatorShared.TokenAuthorization
{
    public interface ITokenAuthorizer
    {
        Token Authorize(string userName, string userPassword);
        bool Authorize(Guid token);
    }
}
