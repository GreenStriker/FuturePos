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
    public class ExpenceTypeController : ControllerBase
    {
        private readonly IExpenceTypeService _service;
     
        private readonly IConfiguration _configuration;
    
        public ExpenceTypeController(
            ControllerBaseParamModel controllerBaseParamModel,
            IExpenceTypeService service
         
            ) : base(controllerBaseParamModel)
        {
            
            _service = service;
            _configuration = Configuration;
       
        }



        public async Task<IActionResult> Index(int? page, string search = null)
        {
            var data = await _service.Query().Where(x => x.IsActive == true).SelectAsync();
            string txt = search;
            //
            if (search != null)
            {
                search = search.ToLower().Trim();
                data = data.Where(c => c.Name.ToLower().Contains(search) || c.Remark.ToString().Contains(search));

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

            return View();
        }

        [HttpPost]


        public async Task<IActionResult> Create(ExpenceType Ep)
        {
            if (Ep.Name.Any())
            {
                Ep.CreatedBy = _session.UserId;
                Ep.CreatedTime = DateTime.Now;
             

                Ep.IsActive = true;


                _service.Insert(Ep);
                await UnitOfWork.SaveChangesAsync();
                TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.SUCCESS_CLASSNAME;
                return RedirectToAction(nameof(Index));
            }
            TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.ERROR_CLASSNAME;
            return View(Ep);
        }




        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var User = await _service.Query()
                .SingleOrDefaultAsync(m => m.ExpenceTypeId == id, CancellationToken.None);
            if (User == null)
            {
                return NotFound();
            }
            return View(User);
        }


        [HttpPost]

        public async Task<IActionResult> Edit(ExpenceType ep)
        {

            if (ep.ExpenceTypeId == 0)
            {
                return NotFound();
            }

            try
            {
                var id = ep.ExpenceTypeId;
                var data = await _service.Query().SingleOrDefaultAsync(m => m.ExpenceTypeId == id, CancellationToken.None);
                //data.IsActive = false;
                //data.DeactiveDate = DateTime.Now;
                //_service.Update(data);
                // data.BranchId=id;
                data.Name = ep.Name;
                data.Remark = ep.Remark;
           
                // data.BranchHeadId = bra.BranchHeadId;
                // data.OpeningDate = bra.OpeningDate;
                //data.DeactiveDate = bra.DeactiveDate;
                // data.IsActive = bra.IsActive;
                data.CreatedBy = _session.UserId;
                data.CreatedTime = DateTime.Now;




                //bra.CreatedBy = _session.UserId;
                //bra.CreatedTime = DateTime.Now;
                //bra.OpeningDate = DateTime.Now;
                //bra.IsActive = true;
                _service.Update(data);
                await UnitOfWork.SaveChangesAsync();

                TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.SUCCESS_CLASSNAME;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.ERROR_CLASSNAME;
                return View(ep);
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
                var data = await _service.Query().SingleOrDefaultAsync(m => m.ExpenceTypeId == id, CancellationToken.None);
                data.IsActive = false;
              
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