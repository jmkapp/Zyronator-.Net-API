using System.Web.Http;
using Zyronator.Models;

namespace Zyronator.Controllers
{
    public class HomeController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Default()
        {
            string baseUrl = Url.Content("~/");

            DefaultUrls defaultUrls = new DefaultUrls();
            defaultUrls.DiscogsLists = baseUrl + "api/discogsuserlists";
            defaultUrls.DatabaseLists = baseUrl + "api/userlists/zyron/database";
            defaultUrls.Synchronize = baseUrl + "api/userlists/synchronize";

            return Ok(defaultUrls);
        }
    }
}