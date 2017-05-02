using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kapitalist.Web.Client.Routes
{
    public static class RouteExtensions
    {
        /// <summary>
        /// Obtains <see cref="Uri"/> by route name.
        /// </summary>
        /// <param name="routeCollection">The route collection.</param>
        /// <param name="routeName">The route name.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>Returns <see cref="Uri"/> instance.</returns>
        //public static Uri GetRouteUri(this RouteCollection routeCollection, string routeName, object routeValues = null)
        //{
        //    var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        //    var routePathUrl = urlHelper.RouteUrl(routeName, routeValues);
        //    return routePathUrl == null ? null : new Uri(HttpContext.Current.Request.Url.GGetHostWithSchemeAndPort() + routePathUrl);
        //}

        /// <summary>
        /// Gets fully qualified URL.
        /// </summary>
        /// <param name="routeCollection">The route collection.</param>
        /// <param name="routeName">The route name.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>Returns <see cref="Uri"/> instance.</returns>
        public static string GetRouteUrl(this RouteCollection routeCollection, string routeName, object routeValues = null)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

            return urlHelper.RouteUrl(routeName, routeValues);
        }

        /// <summary>
        /// Maps a route with a name.
        /// </summary>
        public static Route MapRouteEx(this RouteCollection routes, string name, string url, object defaults)
        {
            return MapRouteEx(routes, name, url, defaults, null);
        }

        /// <summary>
        /// Maps a route with a name.
        /// </summary>
        public static Route MapRouteEx(this RouteCollection routes, string name, string url, string controller, string action, object constraints = null)
        {
            const string controllerName = "Controller";
            if (controller.EndsWith(controllerName))
            {
                controller = controller.Remove(controller.Length - controllerName.Length, controllerName.Length);
            }

            return MapRouteEx(routes, name, url, new { controller, action }, constraints);
        }

        /// <summary>
        /// Maps a route with a name.
        /// </summary>
        public static Route MapRouteEx(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            var route = new Route(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints),
                DataTokens = new RouteValueDictionary()
            };

            route.DataTokens["RouteName"] = name;
            routes.Add(name, route);
            return route;
        }
    }
}