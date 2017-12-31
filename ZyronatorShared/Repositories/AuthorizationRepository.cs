using System;
using System.Linq;
using System.Web.Helpers;
using ZyronatorShared.ConfigurationProperties;
using ZyronatorShared.LocalDataModel;
using ZyronatorShared.PublicDataModel;
using ZyronatorShared.TokenAuthorization;

namespace ZyronatorShared.Repositories
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly IConfigurationProperties _configurationProperties;

        public AuthorizationRepository(IConfigurationProperties configurationProperties)
        {
            _configurationProperties = configurationProperties;
        }

        public Token Authorize(string userName, string userPassword)
        {
            using (var context = new ZyronatorDataContext(_configurationProperties.ConnectionString))
            {
                ApplicationUser appUser = context.ApplicationUsers.SingleOrDefault(us => us.UserName == userName);

                if (appUser == null)
                    return null;

                bool verified = Crypto.VerifyHashedPassword(appUser.UserPassword, userPassword);

                if (!verified)
                    return null;

                appUser.AuthorizationToken = Guid.NewGuid();
                appUser.AuthorizationDate = _configurationProperties.CurrentDate.Date;
                context.SaveChanges();

                return new Token(appUser.AuthorizationToken, appUser.AuthorizationDate);
            }
        }

        public Token Authorize(Guid token)
        {
            using (var context = new ZyronatorDataContext(_configurationProperties.ConnectionString))
            {
                ApplicationUser appUser = context.ApplicationUsers.SingleOrDefault(us => us.AuthorizationToken == token);

                if (appUser == null)
                    return null;

                appUser.AuthorizationToken = Guid.NewGuid();
                appUser.AuthorizationDate = _configurationProperties.CurrentDate.Date;
                context.SaveChanges();

                return new Token(appUser.AuthorizationToken, appUser.AuthorizationDate);
            }
        }
    }
}
