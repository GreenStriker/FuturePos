using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using vms.ActionFilter;
using vms.entity.viewModels;
using vms.ioc;
using Inventory.Utility;

namespace vms
{
    public class Startup
    {
        public static Dictionary<string, string> DefaultPageSize { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
           // services.AddScoped<Logged>();
            DefaultPageSize = Configuration.GetSection("DefaultPageSize").GetChildren().Select(item => new KeyValuePair<string, string>(item.Key, item.Value)).ToDictionary(x => x.Key, x => x.Value);
            services.RegisterVMSServiceInstance(this.Configuration);
            // services.Configure<ReportServer>(Configuration.GetSection("ReportServer"));
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            //});
            services.AddScoped<ControllerBaseParamModel, ControllerBaseParamModel>();
            services.AddHttpContextAccessor();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });




            services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(@"c:\temp-keys\"));
            services.AddScoped<ControllerBaseParamModel, ControllerBaseParamModel>();
            services.AddHttpContextAccessor();
            //services.AddMvc()
            //    .AddJsonOptions(options =>
            //        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);//You can set Time
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false; // Default is true, make it false
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
            //  System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"Libs")),
            //    RequestPath = new PathString("/libs")
            //});
            app.UseCookiePolicy();
            app.UseSession();

            app.UseMvc(routes =>
            {
                //routes.MapRoute
                //(
                //    name: "site",
                //    template: "{site}/{page?}",
                //    constraints: new { site = new SiteRouteConstraint() },
                //    defaults: new { controller = "Site", action = "Index" }
                //);

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Default}/{action=Index}/{id?}");
            });
        }

        //public class GolbalSession
        //{
        //    private ALP _ALP;
        //    private GetUserProgramme _GetUserProgramme;
        //    public static ALP gblSession
        //    {
        //        get { return (ALP)GetFromSession("ALP"); }
        //        set { SetInSession("ALP", value); }
        //    }
        //    //public static GetUserProgramme gblUserProgramme
        //    //{
        //    //    get { return (GetUserProgramme)GetProgramFromSession("GetUserProgramme"); }
        //    //    set { SetProgramInSession("GetUserProgramme", value); }
        //    //}
        //    private static void SetInSession(string key, object value)
        //    {
        //        HttpContext.Session.SetString(key, value);
        //    }
        //    static object GetFromSession(string key)
        //    {
        //        HttpContext.Session.SetString(SessionName, "Nusrat");
        //    }

        //    //private static void SetProgramInSession(string key, object value)
        //    //{
        //    //    HttpContext.Current.Session[key] = value;
        //    //}
        //    //static object GetProgramFromSession(string key)
        //    //{
        //    //    return HttpContext.Current.Session[key];
        //    //}
        //}
    }
}