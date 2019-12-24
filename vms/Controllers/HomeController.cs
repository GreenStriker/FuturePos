using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using vms.entity.models;
using vms.entity.viewModels;
using vms.Models;
using vms.service.dbo;
using vms.utility;
using Inventory.Utility;
using vms.utility.StaticData;

namespace Inventory.Controllers
{
    public class HomeController : ControllerBase
    {

        private readonly IUserService _service;
        //private readonly IRoleRightService _roleRightService;
        //private readonly IRightService _rightService;
        private readonly IConfiguration _configuration;
        //private readonly IOrganizationService _orgcConfiguration;

        public HomeController(
            ControllerBaseParamModel controllerBaseParamModel,
            IUserService service
            //IRoleRightService roleRightService, 
            //IRightService rightService, 
            //IOrganizationService orgcConfiguration
            ) : base(controllerBaseParamModel)
        {

            _service = service;
            _configuration = Configuration;
            //_roleRightService = roleRightService;
            //_rightService = rightService;
            //_orgcConfiguration = orgcConfiguration;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Dashboard()
        {
            var id = _session.BranchId;

            return View();
        }

    }
}