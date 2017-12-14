using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Http;
using Zyronator.Models;
using Zyronator.Repositories;

namespace Zyronator.Controllers
{
    public class UserListsController : ApiController
    {
        private readonly IUserListsRepository _userListsRepository;

        public UserListsController(IUserListsRepository userListsRepository)
        {
            _userListsRepository = userListsRepository;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult Default()
        {
            string baseUrl = Url.Content("~/");

            DefaultUrls defaultUrls = new DefaultUrls();
            defaultUrls.DiscogsLists = baseUrl + "api/userLists/zyron";
            defaultUrls.DatabaseLists = baseUrl + "api/userlists/zyron/database";
            defaultUrls.Synchronize = baseUrl + "api/userlists/synchronize";

            return Ok(defaultUrls);
        }

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
            IEnumerable<DatabaseUserList> databaseUserLists = _userListsRepository.GetUserLists();

            return Ok(databaseUserLists);
        }

        [Route("api/userlists/synchronize")]
        [HttpGet]
        public IHttpActionResult SynchronizeLists()
        {
            List<List> userLists = GetUserLists();

            List<DatabaseUserList> databaseUserLists = _userListsRepository.GetUserLists();

            var notInDatabase = userLists.Where(list1Item => !databaseUserLists.Any(list2Item => list2Item.DiscogsId == list1Item.Id));

            _userListsRepository.Insert(notInDatabase);

            return Ok();
        }
    }
}
