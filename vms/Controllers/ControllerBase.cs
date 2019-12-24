using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using URF.Core.Abstractions;
using vms.entity.models;
using vms.entity.viewModels;
using vms.utility.StaticData;
using Inventory.Utility;

namespace Inventory.Controllers
{
    //[SessionExpireFilter]
    public class ControllerBase : Controller
    {
        protected IHostingEnvironment Environment;
        protected IUnitOfWork UnitOfWork;
        protected static IConfiguration Configuration;
        protected vmSession _session;
        protected readonly IDataProtectionProvider _protectionProvider;
        protected readonly PurposeStringConstants _purposeStringConstants;
        protected IDataProtector _dataProtector;

        protected ControllerBase(ControllerBaseParamModel controllerBaseParamModel)
        {
            Environment = controllerBaseParamModel.HostingEnvironment;
            UnitOfWork = controllerBaseParamModel.UnitOfWork;
            _dataProtector = controllerBaseParamModel.ProtectionProvider.CreateProtector(controllerBaseParamModel.p_purposeStringConstants.UserIdQueryString);
            _session = controllerBaseParamModel.HttpContextAccessor.HttpContext.Session.GetComplexData<vmSession>("session");
            if (Configuration == null)
            {

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Environment.ContentRootPath)
                    .AddJsonFile(ControllerStaticData.APPLICATION_JSON, true, true);
                Configuration = builder.Build();
            }
        }


    }
}