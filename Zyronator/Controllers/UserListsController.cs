using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Http;
using Zyronator.Models;
using Zyronator.Services;

namespace Zyronator.Controllers
{
    public class UserListsController : ApiController
    {
        [Route("api/userlists/zyron")]
        [HttpGet]
        public IHttpActionResult GetAllUserLists()
        {
            var userLists = GetUserLists();
            return Ok(userLists);
        }

        private List<List> GetUserLists()
        {
            List<List> userLists = new List<List>();

            var client = new RestClient
            {
                BaseUrl = new Uri(WebConfigurationManager.AppSettings["DiscogsApiUri"]),
                UserAgent = WebConfigurationManager.AppSettings["UserAgent"]
            };

            string zyron = WebConfigurationManager.AppSettings["DefaultDiscogsUser"];

            var request = new RestRequest();
            request.Resource = "users/{username}/lists";
            request.AddUrlSegment("username", zyron);

            IRestResponse response = client.Execute(request);

            JsonDeserializer deserializer = new JsonDeserializer();
            var rootUserLists = deserializer.Deserialize<RootUserLists>(response);

            userLists.AddRange(rootUserLists.Lists);

            bool more = NextPage(rootUserLists);

            while (more == true)
            {
                Uri nextPageUri = new Uri(rootUserLists.Pagination.Urls.Next);

                client.BaseUrl = nextPageUri;

                request = new RestRequest();

                response = client.Execute(request);

                rootUserLists = deserializer.Deserialize<RootUserLists>(response);

                userLists.AddRange(rootUserLists.Lists);

                more = NextPage(rootUserLists);
            }

            return userLists;
        }

        private bool NextPage(RootUserLists rootUserLists)
        {
            int pages = rootUserLists.Pagination.Pages;
            int page = rootUserLists.Pagination.Page;

            if (page < pages)
            {
                return true;
            }

            return false;
        }

        [Route("api/userlists/zyron/database")]
        [HttpGet]
        public IHttpActionResult GetDatabaseUserLists()
        {
            UserListsService service = new UserListsService();

            IEnumerable<DatabaseUserList> databaseUserLists = service.GetUserLists();

            return Ok(databaseUserLists);
        }

        [Route("api/userlists/synchronize")]
        [HttpGet]
        public IHttpActionResult SynchronizeLists()
        {
            List<List> userLists = GetUserLists();

            UserListsService service = new UserListsService();

            List<DatabaseUserList> databaseUserLists = service.GetUserLists();

            var notInDatabase = userLists.Where(list1Item => !databaseUserLists.Any(list2Item => list2Item.DiscogsId == list1Item.Id));

            service.Insert(notInDatabase);

            return Ok();
        }
    }
}
