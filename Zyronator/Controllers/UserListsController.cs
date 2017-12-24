using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Zyronator.Repositories;
using ZyronatorShared.DiscogsApiModels;

namespace Zyronator.Controllers
{
    public class UserListsController : ApiController
    {
        private readonly IUserListsRepository _userListsRepository;

        public UserListsController(IUserListsRepository userListsRepository)
        {
            _userListsRepository = userListsRepository;
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
            DiscogsUserListsController listController = new DiscogsUserListsController();

            List<List> userLists = listController.Get().ToList();

            List<DatabaseUserList> databaseUserLists = _userListsRepository.GetUserLists();

            var notInDatabase = userLists.Where(list1Item => !databaseUserLists.Any(list2Item => list2Item.DiscogsId == list1Item.Id));

            _userListsRepository.Insert(notInDatabase);

            return Ok();
        }
    }
}
