using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading;
using vms.entity.models;
using vms.entity.viewModels;
using vms.service.dbo;
using vms.utility;
using vms.utility.StaticData;
using Inventory.Utility;
using Microsoft.AspNetCore.Http.Features;

namespace Inventory.Controllers
{
    public class DashboardController : Controller
    {
        private object httpContext;

        public IActionResult Index()
        {

           
            return View();
        }
    }
}