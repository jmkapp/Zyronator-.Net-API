using System.Collections.ObjectModel;
using System.Linq;
using ZyronatorShared.DiscogsApiModels;

namespace ZyronatorShared
{
    public class DiscogsApiAccess : IDiscogsApiAccess
    {
        public DiscogsUserListDetail GetDiscogsList(int listId)
        {
            ZyronatorRestClient restClient = new ZyronatorRestClient();

            DiscogsListDetailFetcher detailsFetcher = new DiscogsListDetailFetcher(restClient);

            return detailsFetcher.Get(listId);
        }

        public ReadOnlyCollection<List> GetZyronDiscogsLists()
        {
            ZyronatorRestClient restClient = new ZyronatorRestClient();

            DiscogsUserListFetcher listsFetcher = new DiscogsUserListFetcher(restClient);

            return new ReadOnlyCollection<List>(listsFetcher.GetUserLists().ToList());
        }
    }
}
