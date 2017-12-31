using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Zyronator.Controllers;
using ZyronatorShared.TokenAuthorization;

namespace Zyronator.Attributes
{
    // http://blog.ploeh.dk/2014/06/13/passive-attributes/

    public class GeneralAuthorizeFilter : IActionFilter
    {
        private readonly ITokenAuthorizer _authorizer;

        public GeneralAuthorizeFilter(ITokenAuthorizer authorizer)
        {
            _authorizer = authorizer;
        }
        
        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var attribute = actionContext
                .ControllerContext
                .ControllerDescriptor
                .GetCustomAttributes<GeneralAuthorizeAttribute>()
                .SingleOrDefault();
            
            if (attribute == null)
                return continuation();

            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return continuation();
            }

            AuthenticationHeaderValue authorization = actionContext.Request.Headers.Authorization;

            if (authorization != null)
            {
                if (authorization.Scheme.Equals("bearer", StringComparison.OrdinalIgnoreCase)
                    && !string.IsNullOrWhiteSpace(authorization.Parameter))
                {
                    string token = authorization.Parameter;

                    bool authorized = _authorizer.Authorize(new Guid(token));

                    if(authorized)
                    {
                        return continuation();
                    }
                }

                if (authorization.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase)
                    && !string.IsNullOrWhiteSpace(authorization.Parameter))
                {
                    string credentials = authorization.Parameter;

                    Encoding encoding = Encoding.GetEncoding("UTF-8");
                    string usernamePassword = encoding.GetString(Convert.FromBase64String(credentials));

                    int seperatorIndex = usernamePassword.IndexOf(':');
                    string username = usernamePassword.Substring(0, seperatorIndex);
                    string password = usernamePassword.Substring(seperatorIndex + 1);

                    var token = _authorizer.Authorize(username, password);

                    // logic to check if OK

                    return continuation();
                }
            }

            //var principal = new GenericPrincipal(new GenericIdentity(username), null);

            //HandleUnauthorized(actionContext);

            //return continuation();

            return Task.FromResult(actionContext.Request.CreateErrorResponse(
            HttpStatusCode.Unauthorized, "Authentication failed."));
        }

        //private void HandleUnauthorized(HttpActionContext actionContext)
        //{
        //    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        //}

        public bool AllowMultiple
        {
            get
            {
                return true;
            }
        }
    }
}