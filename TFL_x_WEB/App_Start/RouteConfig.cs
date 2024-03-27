using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace TFL_x_WEB
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            // settings.AutoRedirectMode = RedirectMode.Permanent;  // Se comenta para que funcionen las llamadas por ajax
            routes.EnableFriendlyUrls(settings);
        }
    }
}
