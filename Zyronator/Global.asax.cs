using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Web.Http;
using System.Web.Http.Filters;
using Zyronator.Attributes;
using Zyronator.Repositories;
using ZyronatorShared;
using ZyronatorShared.Repositories;
using ZyronatorShared.TokenAuthorization;

namespace Zyronator
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register<ZyronatorShared.ConfigurationProperties.IConfigurationProperties, ConfigurationProperties.ConfigurationProperties>(Lifestyle.Singleton);
            container.Register<IDiscogsApiAccess, DiscogsApiAccess>(Lifestyle.Singleton);

            container.Register<IAuthorizationRepository, AuthorizationRepository>(Lifestyle.Singleton);
            container.Register<ITokenAuthorizer, TokenAuthorizer>(Lifestyle.Singleton);

            container.Register<IUserListsRepository, UserListsRepository>(Lifestyle.Scoped);
            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = 
                new SimpleInjectorWebApiDependencyResolver(container);

            var authoriser = container.GetInstance<ITokenAuthorizer>();
            HttpConfiguration httpConfiguration = GlobalConfiguration.Configuration;
            var generalFilter = new GeneralAuthorizeFilter(authoriser);
            httpConfiguration.Filters.Add(generalFilter);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
