///-------------------------------------------------------------///
/// this is a class to check if cookie token is present
///-------------------------------------------------------------///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace REPS.UI
{
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (Authorize(filterContext))
            {
                return;
            }
            HandleUnauthorizedRequest(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", area = "" }));
        }

        private bool Authorize(AuthorizationContext actionContext)
        {
            try
            {
                HttpRequestBase request = actionContext.RequestContext.HttpContext.Request;
                if (request.Params["Bearer"] != null)
                    return true;
                else
                    return false;
                //string token = request.Params["Bearer"];
                //return SecurityManager.IsTokenValid(token, CommonManager.GetIP(request), request.UserAgent);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}