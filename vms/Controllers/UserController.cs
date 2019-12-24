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
using X.PagedList;
using System;

namespace Inventory.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IVatService _vatService;
        private readonly IProductService _prodService;
        //private readonly IRightService _rightService;
        private readonly IConfiguration _configuration;
        //private readonly IOrganizationService _orgcConfiguration;
        private readonly IProductLogService _logService;
        public UserController(
            ControllerBaseParamModel controllerBaseParamModel,
            IUserService service,
            IVatService vatService,
            IProductService prodService,
            IProductLogService logService
            //IRightService rightService, 
            //IOrganizationService orgcConfiguration
            ) : base(controllerBaseParamModel)
        {
            _logService = logService;
            _service = service;
            _configuration = Configuration;
            _vatService = vatService;
            _prodService = prodService;
            //_rightService = rightService;
            //_orgcConfiguration = orgcConfiguration;
        }



        public async Task<IActionResult> Index(int? page, string search = null)
        {
            var data = await _service.Query().Include(x=>x.Brach).Where(x => x.IsActive == true).SelectAsync();
            string txt = search;
            //
            if (search != null)
            {
                search = search.ToLower().Trim();
                data = data.Where(c => c.Name.ToLower().Contains(search) || c.Designation.ToString().Contains(search) || c.Moble.ToString().Contains(search) || c.Brach.Name.ToString().Contains(search) || c.UserName.ToString().Contains(search) || c.Address.ToString().Contains(search));

            }
            if (txt != null)
            {
                ViewData[ViewStaticData.SEARCH_TEXT] = txt;
            }
            else
            {
                ViewData[ViewStaticData.SEARCH_TEXT] = string.Empty;

            }
            var pageNumber = page ?? 1;
            var listOfdata = data.ToPagedList(pageNumber, 10);
            return View(listOfdata);

        }





        public IActionResult Create()
        {
            var data = new User();
            return View(data);
        }

        [HttpPost]


        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.CreatedBy = _session.UserId;
                user.CreatedTime = DateTime.Now;
                user.JoingDate = DateTime.Now;
                user.BrachId = _session.BranchId;
                user.IsActive = true;


                _service.Insert(user);
                await UnitOfWork.SaveChangesAsync();
                TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.SUCCESS_CLASSNAME;
                return RedirectToAction(nameof(Index));
            }
            TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.ERROR_CLASSNAME;
            return View(user);
        }




        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var User = await _service.Query()
                .SingleOrDefaultAsync(m => m.Uid == id, CancellationToken.None);
            if (User == null)
            {
                return NotFound();
            }
            return View(User);
        }


        [HttpPost]

        public async Task<IActionResult> Edit(User user)
        {

            if (user.Uid == 0)
            {
                return NotFound();
            }

            try
            {
                var id = user.Uid;
                var data = await _service.Query().SingleOrDefaultAsync(m => m.Uid == id, CancellationToken.None);


                data.Name = user.Name;
                data.Moble = user.Moble;
                data.Designation = user.Designation;
                data.Address = user.Address;
                data.CreatedBy = _session.UserId;
                data.CreatedTime = DateTime.Now;
                _service.Update(data);
                await UnitOfWork.SaveChangesAsync();
                
                TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.SUCCESS_CLASSNAME;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.ERROR_CLASSNAME;
                return View(user);
            }
        }









        public async Task<IActionResult> Delete(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var data = await _service.Query().SingleOrDefaultAsync(m => m.Uid == id, CancellationToken.None);
                data.IsActive = false;
                data.DeactiveDate = DateTime.Now;
                _service.Update(data);

                await UnitOfWork.SaveChangesAsync();

                TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.DELETE_CLASSNAME;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.ERROR_CLASSNAME;
                return RedirectToAction(nameof(Index));
            }
        }

    }
}