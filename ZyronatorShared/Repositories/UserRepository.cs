using System;
using System.Web.Helpers;
using ZyronatorShared.ConfigurationProperties;
using ZyronatorShared.LocalDataModel;
using ZyronatorShared.PublicDataModel;

namespace ZyronatorShared.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IConfigurationProperties _configurationProperties;

        public UserRepository(IConfigurationProperties configurationProperties)
        {
            _configurationProperties = configurationProperties;
        }

        public ZyronatorUser AddUser(NewZyronatorUser user)
        {
            ApplicationUser newUser = new ApplicationUser();
            newUser.UserName = user.UserName;
            newUser.UserPassword = Crypto.HashPassword(user.Password);

            int newId = 0;

            using (var context = new ZyronatorDataContext(_configurationProperties.ConnectionString))
            {
                context.Database.Log = Console.WriteLine;
                context.ApplicationUsers.Add(newUser);
                context.SaveChanges();

                newId = newUser.UserId;
            }

            return new ZyronatorUser(newId, newUser.UserName);
        }
    }
}
