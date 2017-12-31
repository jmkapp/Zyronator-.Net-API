using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Zyronator.Controllers;
//using ZyronatorShared;
//using ZyronatorShared.TokenAuthorization;

namespace ZyronatorTests.ControllerTests
{
    [TestClass]
    public class DiscogsUserListsControllerTests
    {
        [TestMethod]
        public void Get_NoAuthorizationHeader_ReturnsUnauthorized()
        {
            //var mockAuthorizer = new Mock<ITokenAuthorizer>();
            //var mockApi = new Mock<IDiscogsApiAccess>();

            //DiscogsUserListsController controller = new DiscogsUserListsController(mockApi.Object);
            //var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:57694/api/discogsuserlists");

            //IHttpActionResult result = controller.Get();

            //var contentResult = result as UnauthorizedResult;

            //Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public void Get()
        {
            ////var mockAuthorizer = new Mock<ITokenAuthorizer>();
            //var mockApi = new Mock<IDiscogsApiAccess>();

            //DiscogsUserListsController controller = new DiscogsUserListsController(mockApi.Object);
            //var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:57694/api/discogsuserlists");
            //request.Headers.Add("Authorization", "Bearer 202e5f0a-5b6f-4571-b84d-53b03b615004");
            //controller.Request = request;

            //IHttpActionResult result = controller.Get();

            //var contentResult = result as UnauthorizedResult;

            //Assert.IsNotNull(contentResult);
        }
    }
}
