using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Web.Http;
using Zyronator.Models;

namespace Zyronator.Controllers
{
    public class UserListsController : ApiController
    {
        public IHttpActionResult GetUserLists()
        {
            var client = new RestClient
            {
                BaseUrl = new Uri("https://api.discogs.com"),
                UserAgent = "Zyronator/0.1"
            };

            string zyron = "Zyron";

            var request = new RestRequest();
            request.Resource = "users/{username}/lists";
            request.AddUrlSegment("username", zyron);

            IRestResponse response = client.Execute(request);

            JsonDeserializer deserializer = new JsonDeserializer();
            var userLists = deserializer.Deserialize<RootObject>(response);

            return Ok(userLists);
        }
    }
}
