using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Zyronator.Attributes
{
    public class GeneralAuthorizeAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
        }
    }
}