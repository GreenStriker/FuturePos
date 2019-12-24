using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using URF.Core.Abstractions;
using vms.api.Utility;

namespace vms.api.Controllers
{
    public class ControllerBase:Controller
    {
        protected IHostingEnvironment _environment;
        protected IUnitOfWork _unitOfWork;
        static protected IConfiguration _configuration;
        protected string ClassName { get; set; }
        protected ControllerBase(IHostingEnvironment hostingEnvironment, IUnitOfWork unitOfWork)
        {
            _environment = hostingEnvironment;
            this._unitOfWork = unitOfWork;

            if (_configuration == null) {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(this._environment.ContentRootPath)
                    .AddJsonFile("applicationsettings.json", true, true);
                _configuration = builder.Build();
            }
        }
        public async Task<FileSaveFeedbackDto> FileSaveAsync(IFormFile File)
        {
            FileSaveFeedbackDto fdto = new FileSaveFeedbackDto();
            var FileExtenstion = Path.GetExtension(File.FileName);

            string FileName = Guid.NewGuid().ToString();

            FileName += FileExtenstion;
            string organizationName = Convert.ToString(await GetCompanyNameFromClaim());
            var FolderName = organizationName;
            var uploads = Path.Combine(_environment.WebRootPath, FolderName);

            fdto.MimeType = FileExtenstion;
            bool exists = Directory.Exists(uploads);
            if (!exists)
            {
                Directory.CreateDirectory(uploads);
            }
            if (File.Length > 0)
            {
                var filePath = Path.Combine(uploads, File.FileName);
                fdto.FileUrl = filePath;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await File.CopyToAsync(fileStream);
                }
            }
            return fdto;
        }
        protected async Task<string> GetEmailFromClaim()
        {
            var principal = this.HttpContext.User;
            if (principal == null) throw new Exception("User's Compnay Cannot found");
            var claims = principal.Claims.ToList();
            var email = claims.FirstOrDefault(c => c.Type == "email")?.Value;

            return await System.Threading.Tasks.Task.FromResult(email);
        }
        protected async Task<int> GetCompanyIdFromClaim()
        {
            var principal = this.HttpContext.User;
            if (principal == null) throw new Exception("Compnay not found");
            var claims = principal.Claims.ToList();
            var companyId = claims.FirstOrDefault(c => c.Type == "CompanyId")?.Value;
            return await System.Threading.Tasks.Task.FromResult(Convert.ToInt32(companyId));
        }
        protected async Task<string> GetStorageNameFromClaim()
        {
            var principal = this.HttpContext.User;
            if (principal == null) throw new Exception("Compnay not found");
            var claims = principal.Claims.ToList();
            var storageName = claims.FirstOrDefault(c => c.Type == "StorageName")?.Value;
            return await System.Threading.Tasks.Task.FromResult(storageName);
        }
        protected async Task<string> GetCompanyNameFromClaim()
        {
            var principal = this.HttpContext.User;
            if (principal == null) throw new Exception("Compnay not found");
            var claims = principal.Claims.ToList();
            var companyName = claims.FirstOrDefault(c => c.Type == "CompanyName")?.Value;
            return await System.Threading.Tasks.Task.FromResult(companyName);
        }
        protected async Task<int> GetUserIdFromClaim()
        {
            var principal = this.HttpContext.User;
            if (principal == null) throw new Exception("User not found");
            var claims = principal.Claims.ToList();
            var userId = claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            return await System.Threading.Tasks.Task.FromResult(Convert.ToInt32(userId));
        }
        protected async Task<string> GetUserNameFromClaim()
        {
            var principal = this.HttpContext.User;
            if (principal == null) throw new Exception("User not found");
            var claims = principal.Claims.ToList();
            var username = claims.FirstOrDefault(c => c.Type == "UserName")?.Value;
            return await System.Threading.Tasks.Task.FromResult(username);
        }
        protected async Task<Dictionary<string, string>> GetClaimsData()
        {
            var principal = this.HttpContext.User;
            var claims = principal.Claims.ToList();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("username", claims.FirstOrDefault(c => c.Type == "UserName")?.Value);
            dictionary.Add("companyname", claims.FirstOrDefault(c => c.Type == "CompanyName")?.Value);
            dictionary.Add("email", claims.FirstOrDefault(c => c.Type == "Email")?.Value);
            return await System.Threading.Tasks.Task.FromResult(dictionary);
        }
        protected static T CopyToJson<T>(T source)
        {

            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace }; //, NullValueHandling = NullValueHandling.Ignore, ObjectCreationHandling = ObjectCreationHandling.Replace

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }), deserializeSettings);
        }
        protected static T DeserializeObject<T>(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return default(T);
            }
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            return JsonConvert.DeserializeObject<T>(source, deserializeSettings);
        }
        protected static IEnumerable<int> StringToIntList(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                yield break;
            }

            var chunks = str.Split(',').AsEnumerable();

            using (var rator = chunks.GetEnumerator())
            {
                while (rator.MoveNext())
                {
                    int i = 0;

                    if (Int32.TryParse(rator.Current, out i))
                    {
                        yield return i;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        protected string GetToken()
        {
            return HttpContext.Request.Headers["authorization"];
        }
        protected string SyncServiceBase()
        {
            return _configuration["Sync_Service_Base"];
        }
        protected WebRequest MakeWebRequest(string url, string contentType = "text/xml")
        {
            var request = WebRequest.CreateHttp(url);
            request.Method = "GET";
            request.Timeout = 30000;
            request.KeepAlive = false;
            request.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            request.Accept = "*/*";
            request.ContentType = contentType;
            request.Headers.Add("Accept-Language", "en-us\r\n");
            request.Headers.Add("Cache-Control", "no-cache\r\n");
            return request;
        }
      
    }
}
