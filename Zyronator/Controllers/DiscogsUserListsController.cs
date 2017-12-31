using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using Zyronator.Attributes;
using ZyronatorShared;
using ZyronatorShared.DiscogsApiModels;
using ZyronatorShared.TokenAuthorization;

namespace Zyronator.Controllers
{
    [GeneralAuthorize]
    public class DiscogsUserListsController : ApiController
    {
        private readonly IDiscogsApiAccess _apiAcccess;

        public DiscogsUserListsController(IDiscogsApiAccess apiAccess)
        {
            _apiAcccess = apiAccess;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            //AuthenticationHeaderValue authorization;

            //var user = this.User;

            //try
            //{
            //    authorization = Request.Headers.Authorization;
            //    var parameter = authorization.Parameter;
            //}
            //catch (NullReferenceException)
            //{
            //    return Unauthorized();
            //}

            //bool authorized = _authorizer.Authorize(new Guid(token));

            //if (!authorized)
            //    return Unauthorized();

            return Ok(_apiAcccess.GetZyronDiscogsLists());
        }

        // GET api/<controller>/5
        public DiscogsUserListDetail Get(int id)
        { 
            return _apiAcccess.GetDiscogsList(id);
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