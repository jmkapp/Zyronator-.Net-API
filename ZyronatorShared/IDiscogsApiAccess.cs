using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZyronatorShared.DiscogsApiModels;

namespace ZyronatorShared
{
    public interface IDiscogsApiAccess
    {
        ReadOnlyCollection<List> GetZyronDiscogsLists();
        DiscogsUserListDetail GetDiscogsList(int listId);
    }
}
