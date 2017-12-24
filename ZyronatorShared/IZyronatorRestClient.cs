using RestSharp;
using System;

namespace ZyronatorShared
{
    public interface IZyronatorRestClient
    {
        Uri BaseUrl
        {
            get;  set;
        }

        IRestResponse Execute(IRestRequest request);
    }
}
