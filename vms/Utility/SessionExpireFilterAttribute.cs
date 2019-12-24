using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Utility
{
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var session = filterContext.HttpContext.Session.GetComplexData<entity.viewModels.vmSession>("session");
            //if (session == null)
            //{

               

            //    // check if a new session id was generated
            //    filterContext.Result = new RedirectResult("~/");
            //    return;
            //}

          
          


            //base.OnActionExecuting(filterContext);
        }
    }
}
