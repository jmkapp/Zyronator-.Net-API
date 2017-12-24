using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using ZyronatorShared;

namespace ZyronatorSharedTests
{
    [TestClass]
    public class DiscogsListDetailFetcherTests
    {
        [TestMethod]
        public void Get_ReturnsListDetail()
        {
            var mockRestClient = new Mock<IZyronatorRestClient>();
            var mockRestResponse = new Mock<IRestResponse>();

            mockRestResponse.Setup(resp => resp.Content).Returns(content);
            mockRestClient.Setup(rest => rest.Execute(It.IsAny<IRestRequest>()))
                .Returns(mockRestResponse.Object);

            DiscogsListDetailFetcher fetcher = new DiscogsListDetailFetcher(mockRestClient.Object);

            var listDetail = fetcher.Get(373143);

            mockRestClient.Verify(rest => rest.Execute(It.IsAny<IRestRequest>()), Times.Once);
            mockRestResponse.Verify(resp => resp.Content, Times.Once);

            Assert.IsNotNull(listDetail);
            Assert.IsTrue(listDetail.Items.Count == 1);
            Assert.AreEqual("(150613) Zyron Live on ISFM", listDetail.Name);
            Assert.AreEqual("No Artist - Environments(New Concepts In Stereo Sound - Disc 1)", listDetail.Items[0].DisplayTitle);
        }

        private readonly string content = "{\"name\": \"(150613) Zyron Live on ISFM\", " +
            "\"items\": [{\"comment\": \"\", \"display_title\": \"No Artist - Environments(New Concepts In Stereo Sound - Disc 1)\", " +
            "\"uri\": \"https://www.discogs.com/No-Artist-Environments-New-Concepts-In-Stereo-Sound-Disc-1/release/345315\", " +
            "\"image_url\": \"\", \"resource_url\": \"https://api.discogs.com/releases/345315\", \"type\": \"release\", \"id\": 345315}], " +
            "\"uri\": \"https://www.discogs.com/lists/150613-Zyron-Live-on-ISFM/373143\", \"id\": 373143, " +
            "\"date_added\": \"2017-11-11T20:08:14-08:00\", \"date_changed\": \"2017-11-11T20:34:44-08:00\", " +
            "\"resource_url\": \"https://api.discogs.com/lists/373143\", \"public\": true, " +
            "\"description\": \"Records played in this stream.\nhttp://zyron.c64.org/mixinfo.php?mixid=182&t=dj-zyron-live-on-isfm-2015-06-13\"}";
    }
}
