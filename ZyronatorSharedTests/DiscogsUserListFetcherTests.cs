using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System.Collections.Generic;
using ZyronatorShared;
using ZyronatorShared.DiscogsApiModels;

namespace ZyronatorSharedTests
{
    [TestClass]
    public class DiscogsUserListFetcherTests
    {
        [TestMethod]
        public void GetUserLists_ReturnsUserLists()
        {
            var mockRestClient = new Mock<IZyronatorRestClient>();
            var mockRestResponse1 = new Mock<IRestResponse>();
            var mockRestResponse2 = new Mock<IRestResponse>();

            mockRestResponse1.Setup(resp => resp.Content).Returns(contentPage1);
            mockRestResponse2.Setup(resp => resp.Content).Returns(contentPage2);

            mockRestClient.SetupSequence(rest => rest.Execute(It.IsAny<IRestRequest>()))
                .Returns(mockRestResponse1.Object)
                .Returns(mockRestResponse2.Object);

            DiscogsUserListFetcher fetcher = new DiscogsUserListFetcher(mockRestClient.Object);

            List<List> userLists = new List<List>(fetcher.GetUserLists());

            mockRestClient.Verify(rest => rest.Execute(It.IsAny<IRestRequest>()), Times.Exactly(2));
            mockRestResponse1.Verify(resp => resp.Content, Times.Once);
            mockRestResponse2.Verify(resp => resp.Content, Times.Once);

            Assert.IsNotNull(userLists);
            Assert.IsTrue(userLists.Count == 2);
            Assert.AreEqual("(150613) Zyron Live on ISFM", userLists[0].Name);
            Assert.AreEqual("Tropical Space Pool Behaviour", userLists[1].Name);
        }

        private readonly string contentPage1 = "{\"pagination\": {\"per_page\": 1, \"pages\": 2, \"page\": 1, " +
                "\"urls\": {\"last\": \"https://api.discogs.com/users/zyron/lists?per_page=1&page=2\", " +
                "\"next\": \"https://api.discogs.com/users/zyron/lists?per_page=1&page=2\"}, \"items\": 2}, " +
                "\"lists\": [{\"name\": \"(150613) Zyron Live on ISFM\", " +
                "\"uri\": \"https://www.discogs.com/lists/150613-Zyron-Live-on-ISFM/373143\", \"id\": 373143, " +
                "\"date_added\": \"2017-11-11T20:08:14-08:00\", \"date_changed\": \"2017-11-11T20:34:44-08:00\", " +
                "\"resource_url\": \"https://api.discogs.com/lists/373143\", \"public\": true, " +
                "\"description\": \"Records played in this stream.\nhttp://zyron.c64.org/mixinfo.php?mixid=182&t=dj-zyron-live-on-isfm-2015-06-13\"}]}";

        private readonly string contentPage2 = "{\"pagination\": {\"per_page\": 1, \"items\": 2, \"page\": 2, " +
            "\"urls\": {\"prev\": \"https://api.discogs.com/users/zyron/lists?per_page=1&page=1\", " +
            "\"first\": \"https://api.discogs.com/users/zyron/lists?per_page=1&page=1\"}, \"pages\": 2}, " +
            "\"lists\": [{\"public\": true, \"name\": \"Tropical Space Pool Behaviour\", \"date_changed\": \"2014-09-20T05:08:59-07:00\", " +
            "\"date_added\": \"2014-09-20T05:05:53-07:00\", \"resource_url\": \"https://api.discogs.com/lists/209831\", " +
            "\"uri\": \"https://www.discogs.com/lists/Tropical-Space-Pool-Behaviour/209831\", \"id\": 209831, " +
            "\"description\": \"Records used in this mix: http://zyron.c64.org/mixinfo.php?mixid=170&t=tropical-space-pool-behaviour\"}]}";
    }
}
