//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace vms.Models
//{

//    /// <summary>
//    /// Provides the APIs for query client access device.
//    /// </summary>
//    public class DetectionService : IDetectionService
//    {
//        public HttpContext Context { get; }
//        public IUserAgent UserAgent { get; }

//        public DetectionService(IServiceProvider services)
//        {
//            if (services == null) throw new ArgumentNullException(nameof(services));

//            this.Context = services.GetRequiredService<IHttpContextAccessor>().HttpContext;
//            this.UserAgent = CreateUserAgent(this.Context);
//        }

//        private IUserAgent CreateUserAgent(HttpContext context)
//        {
//            if (context == null) throw new ArgumentNullException(nameof(Context));

//            return new UserAgent(Context.Request.Headers["User-Agent"].FirstOrDefault());
//        }
//    }

//    public interface IDetectionService
//    {


//    }
//}
