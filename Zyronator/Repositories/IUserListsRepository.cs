using System.Collections.Generic;
using Zyronator.Models;
using ZyronatorShared.DiscogsApiModels;

namespace Zyronator.Repositories
{
    public interface IUserListsRepository
    {
        List<DatabaseUserList> GetUserLists();
        void Insert(IEnumerable<List> userLists);
    }
}
