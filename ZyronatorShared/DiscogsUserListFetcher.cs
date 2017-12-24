using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using ZyronatorShared.DiscogsApiModels;

namespace ZyronatorShared
{
    public class DiscogsUserListFetcher
    {
        // I know that hard coded strings are bad practice
        // but there is currently a bug that prevents creating a Settings file
        // see https://github.com/dotnet/project-system/issues/2737

        private readonly string _resource = "users/{username}/lists";
        private readonly string _defaultDiscogsUser = "zyron";

        private readonly IZyronatorRestClient _restClient;

        public DiscogsUserListFetcher(IZyronatorRestClient restClient)
        {
            _restClient = restClient;
        }

        public IEnumerable<List> GetUserLists()
        {
            List<List> userLists = new List<List>();

            IRestRequest request = new RestRequest();
            request.Resource = _resource;
            request.AddUrlSegment("username", _defaultDiscogsUser);

            IRestResponse response = _restClient.Execute(request);

            JsonDeserializer deserializer = new JsonDeserializer();
            var rootUserLists = deserializer.Deserialize<DiscogsUserLists>(response);

            userLists.AddRange(rootUserLists.Lists);

            bool more = NextPage(rootUserLists);

            while (more == true)
            {
                Uri nextPageUri = new Uri(rootUserLists.Pagination.Urls.Next);

                _restClient.BaseUrl = nextPageUri;

                request = new RestRequest();

                response = _restClient.Execute(request);

                rootUserLists = deserializer.Deserialize<DiscogsUserLists>(response);

                userLists.AddRange(rootUserLists.Lists);

                more = NextPage(rootUserLists);
            }

            return userLists;
        }

        private bool NextPage(DiscogsUserLists rootUserLists)
        {
            int pages = rootUserLists.Pagination.Pages;
            int page = rootUserLists.Pagination.Page;

            if (page < pages)
            {
                return true;
            }

            return false;
        }
    }
}
