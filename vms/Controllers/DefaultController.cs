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


namespace Inventory.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IUserService _service;
        private readonly IThemeService _themeservice;
        public DefaultController(
          IThemeService themeservice,
        IUserService service
          )
        {
            _themeservice = themeservice;
            _service = service;
         
        }





        public async Task<IActionResult> Index()
        {

           // var userData = await _service.GetUser(1);


            return View();



        }

        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
           
          

        var userData = await _service.Query().Include(c=> c.Brach).SingleOrDefaultAsync(w => w.UserName.ToLower() == user.UserName.Trim().ToLower() && w.Password == user.Password  && w.IsActive==true , CancellationToken.None);


            var theme = await _themeservice.Query().Include(c => c.Color).SingleOrDefaultAsync(w => w.Uid== userData.Uid, CancellationToken.None);



            if (userData != null)
            {

               if(theme != null) { 
                    var session = new vmSession
                    {

                          UserId = userData.Uid,
                          UserName = userData.Name,
                          BranchId = userData.BrachId,
                          BranchName = userData.Brach.Name,
                          FristThe = theme.Color.Frist,
                          secThe = theme.Color.Sec,
                          thirdThe = theme.Color.Third,
                          FroreThe =theme.Color.Forth,



                    };

                    HttpContext.Session.SetComplexData(vms.utility.StaticData.ControllerStaticData.SESSION, session);
                }


                else
                {
                    
                    var session = new vmSession
                    {

                        UserId = userData.Uid,
                        UserName = userData.Name,
                        BranchId = userData.BrachId,
                        BranchName = userData.Brach.Name,
                        FristThe = theme.Color.Frist,
                        secThe = theme.Color.Sec,
                        thirdThe = theme.Color.Third,
                        FroreThe = theme.Color.Forth,
                        
                    };

                    HttpContext.Session.SetComplexData(vms.utility.StaticData.ControllerStaticData.SESSION, session);
                }

               


                
                return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                return RedirectToAction("Index", "Default");
                 }
            
        

            //return RedirectToAction(ControllerStaticData.DISPLAY_INDEX, ControllerStaticData.AUTHENTICATION);
        }













    }
}