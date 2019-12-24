using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using URF.Core.Abstractions;
using vms.entity.viewModels;

namespace Inventory.Utility
{
    public class ControllerBaseParamModel
    {
        public ControllerBaseParamModel(PurposeStringConstants _purposeStringConstants, IDataProtectionProvider p_protectionProvider, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            HostingEnvironment = hostingEnvironment;
            HttpContextAccessor = httpContextAccessor;
            UnitOfWork = unitOfWork;
            ProtectionProvider = p_protectionProvider;
            p_purposeStringConstants = _purposeStringConstants;
        }

        public IHostingEnvironment HostingEnvironment { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }
        public IUnitOfWork UnitOfWork { get; }
        public IDataProtectionProvider ProtectionProvider { get; }
        public PurposeStringConstants p_purposeStringConstants { get; }

    }
}