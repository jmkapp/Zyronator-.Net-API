using RestSharp;
using RestSharp.Deserializers;
using ZyronatorShared.DiscogsApiModels;

namespace ZyronatorShared
{
    public class DiscogsListDetailFetcher
    {
        // I know that hard coded strings are bad practice
        // but there is currently a bug that prevents creating a Settings file
        // see https://github.com/dotnet/project-system/issues/2737

        private readonly string _resource = "lists/{listId}";

        private readonly IZyronatorRestClient _restClient;

        public DiscogsListDetailFetcher(IZyronatorRestClient restClient)
        {
            _restClient = restClient;
        }

        public DiscogsUserListDetail Get(int id)
        {
            var request = new RestRequest();
            request.Resource = _resource;
            request.AddUrlSegment("listId", id);

            IRestResponse response = _restClient.Execute(request);

            JsonDeserializer deserializer = new JsonDeserializer();
            var listDetail = deserializer.Deserialize<DiscogsUserListDetail>(response);

            return listDetail;
        }
    }
}
