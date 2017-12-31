using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using ZyronatorShared.ConfigurationProperties;
using ZyronatorShared.PublicDataModel;
using ZyronatorShared.Repositories;
using ZyronatorShared.TokenAuthorization;

namespace ZyronatorSharedTests
{
    [TestClass]
    public class TokenAuthorizerTests
    {
        [TestMethod]
        public void Authorize_UserNameAndPasswordSupplied_RepositoryReturnsNullToken()
        {
            var mockAuthorizationRepository = new Mock<IAuthorizationRepository>();
            var mockConfiguration = new Mock<IConfigurationProperties>();

            TokenAuthorizer authorizer = new TokenAuthorizer(mockConfiguration.Object, mockAuthorizationRepository.Object);

            mockAuthorizationRepository.Setup(rep => rep.Authorize("testName", "testPassword")).Returns((Token)null);

            Token token = authorizer.Authorize("testName", "testPassword");

            Assert.IsNull(token);
            mockAuthorizationRepository.Verify(rep => rep.Authorize(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            mockAuthorizationRepository.Verify(rep => rep.Authorize("testName", "testPassword"), Times.Once);
        }

        [TestMethod]
        public void Authorize_UserNameAndPasswordSupplied_RepositoryReturnsToken()
        {
            var mockAuthorizationRepository = new Mock<IAuthorizationRepository>();
            var mockConfiguration = new Mock<IConfigurationProperties>();

            TokenAuthorizer authorizer = new TokenAuthorizer(mockConfiguration.Object, mockAuthorizationRepository.Object);

            Token token = new Token(new Guid("b2b7bb51-fcba-4c50-b4e2-712cc96b9b2e"), new DateTime(2001, 01, 01));
            mockAuthorizationRepository.Setup(rep => rep.Authorize("testUsername", "testPassword")).Returns(token);

            Token newToken = authorizer.Authorize("testUsername", "testPassword");

            Assert.IsNotNull(newToken);
            Assert.AreEqual(token.AuthorisationToken, newToken.AuthorisationToken);
            mockAuthorizationRepository.Verify(rep => rep.Authorize(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            mockAuthorizationRepository.Verify(rep => rep.Authorize("testUsername", "testPassword"), Times.Once);
        }

        //[TestMethod]
        //public void Authorize_ValidToken_ReAuthorizeFromCache()
        //{
        //    Guid existingGuid = new Guid("17e18c3e-d861-488f-8fda-99fea3a08502");
        //    var mockAuthorizationRepository = new Mock<IAuthorizationRepository>();
        //    var mockConfiguration = new Mock<IConfigurationProperties>();
        //    TokenAuthorizer authorizer = new TokenAuthorizer(mockConfiguration.Object, mockAuthorizationRepository.Object);

        //    Token token = new Token(new Guid("b2b7bb51-fcba-4c50-b4e2-712cc96b9b2e"), new DateTime(2001, 01, 01));
        //    mockAuthorizationRepository.Setup(rep => rep.Authorize(existingGuid)).Returns(token);
        //    mockConfiguration.Setup(rep => rep.CurrentDate).Returns(new DateTime(2001, 01, 01));

        //    // Create initial token from userName & password
        //    Token newToken = authorizer.Authorize("testUserName", "testPassword");
        //    // reauthorize from cached tokens (no database call)
        //    bool newToken2 = authorizer.Authorize(newToken.AuthorisationToken);

        //    Assert.IsNotNull(newToken);
        //    Assert.IsTrue(newToken2);
        //    Assert.AreEqual(token.AuthorisationToken, newToken.AuthorisationToken);
        //    mockConfiguration.Verify(rep => rep.CurrentDate, Times.Once);
        //    mockAuthorizationRepository.Verify(rep => rep.Authorize(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        //    mockAuthorizationRepository.Verify(rep => rep.Authorize("testUserName", "testPassword"), Times.Once);
        //}

        //[TestMethod]
        //public void Authorize_ExpiredToken_ReAuthorizes()
        //{
        //    var mockUserRepository = new Mock<IUserRepository>();
        //    TokenAuthorizer authorizer = new TokenAuthorizer(mockUserRepository.Object);
        //    Guid initialToken = new Guid("b2b7bb51-fcba-4c50-b4e2-712cc96b9b2e");

        //    ZyronatorUser user = new ZyronatorUser(12345, "testUserName");
        //    Token token1 = new Token(user, initialToken, new DateTime(2001, 01, 01));
        //    Token token2 = new Token(user, new Guid("2a4c3f53-a57e-40ca-b09c-5a907abb4fab"), new DateTime(2002, 01, 01));

        //    mockUserRepository.Setup(rep => rep.Authorize("testUserName", "testPassword")).Returns(token1);
        //    mockUserRepository.Setup(rep => rep.Authorize("testUserName", initialToken)).Returns(token2);
        //    mockUserRepository.Setup(rep => rep.CurrentDate).Returns(new DateTime(2002, 01, 01));

        //    // Initial token
        //    Token newToken = authorizer.Authorize("testUserName", "testPassword");
        //    // Reauthorized token
        //    Token newToken2 = authorizer.Authorize("testUserName", initialToken);

        //    Assert.IsNotNull(newToken);
        //    Assert.IsNotNull(newToken2);
        //    Assert.AreEqual(token1.AuthorisationToken, newToken.AuthorisationToken);
        //    Assert.AreEqual(token2.AuthorisationToken, newToken2.AuthorisationToken);
        //    mockUserRepository.Verify(rep => rep.CurrentDate, Times.Once);
        //    mockUserRepository.Verify(rep => rep.Authorize("testUserName", "testPassword"), Times.Once);
        //    mockUserRepository.Verify(rep => rep.Authorize(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        //    mockUserRepository.Verify(rep => rep.Authorize("testUserName", initialToken), Times.Once);
        //    mockUserRepository.Verify(rep => rep.Authorize(It.IsAny<string>(), It.IsAny<Guid>()), Times.Once);
        //}
    }
}
