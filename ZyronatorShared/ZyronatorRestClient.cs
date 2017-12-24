using System;
using RestSharp;

namespace ZyronatorShared
{
    public class ZyronatorRestClient : IZyronatorRestClient
    {
        // I know that hard coded strings are bad practice
        // but there is currently a bug that prevents creating a Settings file
        // see https://github.com/dotnet/project-system/issues/2737

        private readonly string _discogsApiBaseUrl = "https://api.discogs.com";
        private readonly string _userAgent = "Zyronator/0.1";

        private readonly RestClient _restClient;

        public ZyronatorRestClient()
        {
            _restClient = new RestClient
            {
                BaseUrl = new Uri(_discogsApiBaseUrl),
                UserAgent = _userAgent
            };
        }

        public System.Uri BaseUrl
        {
            get
            {
                return _restClient.BaseUrl;
            }

            set
            {
                _restClient.BaseUrl = value;
            }
        }
            
        public IRestResponse Execute(IRestRequest request)
        {
            return _restClient.Execute(request);
        }
    }
}
