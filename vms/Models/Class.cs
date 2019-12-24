//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Routing;

//public class SiteRouteConstraint : IRouteConstraint
//{
//    public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
//    {
//        var site = values[routeKey]?.ToString();
//        return SiteService.SiteExists(site);
//    }
//}