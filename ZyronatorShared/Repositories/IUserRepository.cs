using System;
using ZyronatorShared.PublicDataModel;
using ZyronatorShared.TokenAuthorization;

namespace ZyronatorShared.Repositories
{
    public interface IUserRepository
    {
        ZyronatorUser AddUser(NewZyronatorUser user);
    }
}
