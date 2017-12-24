using System.Collections.Generic;
using System.Web.Http;
using ZyronatorShared;
using ZyronatorShared.DiscogsApiModels;

namespace Zyronator.Controllers
{
    public class DiscogsUserListsController : ApiController
    {
        private readonly ZyronatorRestClient _restClient;

        public DiscogsUserListsController()
        {
            _restClient = new ZyronatorRestClient();
        }

        // GET api/<controller>
        public IEnumerable<List> Get()
        {
            DiscogsUserListFetcher listsFetcher = new DiscogsUserListFetcher(_restClient);

            return listsFetcher.GetUserLists();
        }

        // GET api/<controller>/5
        public DiscogsUserListDetail Get(int id)
        {
            DiscogsListDetailFetcher detailsFetcher = new DiscogsListDetailFetcher(_restClient);

            return detailsFetcher.Get(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}