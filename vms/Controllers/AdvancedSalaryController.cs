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
    public class AdvancedSalaryController : ControllerBase
    {



        private readonly ISettingService _setservice;
        private readonly IAdvancedSalaryService _service;
        private readonly IEmployeService _Empservice;
        private readonly IConfiguration _configuration;
   
        public AdvancedSalaryController(
            ControllerBaseParamModel controllerBaseParamModel,
            ISettingService setservice,
        IAdvancedSalaryService service,
               IEmployeService Empservice

            ) : base(controllerBaseParamModel)
        {

            _service = service;
            _Empservice = Empservice;

            _setservice = setservice;
        }



        public async Task<IActionResult> Index(int? page, string search = null)
        {


            var set =await _setservice.Query().SingleOrDefaultAsync(m => m.IsActive == true, CancellationToken.None);


            var data = await _service.Query().Include(c=> c.Emloy).Include(c => c.Emloy.Branch).Where(x=> x.IsActive == true).SelectAsync();
            string txt = search;

            if (search != null)
            {
                search = search.ToLower().Trim();
                data = data.Where(c => c.Emloy.Name.ToLower().Contains(search) || c.DateTaben.ToString().Contains(search));

            }
            if (txt != null)
            {
                ViewData[ViewStaticData.SEARCH_TEXT] = txt;
            }
            else
            {
                ViewData[ViewStaticData.SEARCH_TEXT] = string.Empty;

            }
            ViewBag.Active = set.IsadvanceSalary;


            var pageNumber = page ?? 1;
            var listOfdata = data.ToPagedList(pageNumber, 10);
            return View(listOfdata);

        }





        public async Task<IActionResult> Create()
        {


            var Employe = await _Empservice.GetAll();
         
            IEnumerable<SelectListItems> Employes = Employe.Where(c => c.IsActive == true && c.BranchId == _session.BranchId).Select(s => new SelectListItems
            {
                Id = s.EmployeId,
                Name = s.Name
            });


            var advanced = new AdvancedSalary()
            {

                Employes = Employes



            };
            return View(advanced);
        }

        [HttpPost]


        public async Task<IActionResult> Create(AdvancedSalary vat)
        {

            var set = await _setservice.Query().SingleOrDefaultAsync(m => m.IsActive == true, CancellationToken.None);
            if (ModelState.IsValid  && set.IsadvanceSalary==true )
            {
                vat.CreatedBy = _session.UserId;
                vat.CreatedTime = DateTime.Now;
                

                vat.IsActive = true;

                _service.Insert(vat);
                await UnitOfWork.SaveChangesAsync();
                TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.SUCCESS_CLASSNAME;
                return RedirectToAction(nameof(Index));
            }
            TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.ERROR_CLASSNAME;
            return View(vat);
        }

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var exportType = await _vatService.Query()
        //        .SingleOrDefaultAsync(m => m.VatId == id, CancellationToken.None);
        //    if (exportType == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(exportType);
        //}


        //[HttpPost]

        //public async Task<IActionResult> Edit(Vat vat)
        //{

        //    if (vat.VatId == 0)
        //    {
        //        return NotFound();
        //    }

        //    try
        //    {
        //        var id = vat.VatId;
        //        var data = await _vatService.Query().SingleOrDefaultAsync(m => m.VatId == id, CancellationToken.None);
        //        data.IsActive = false;
        //        data.EfectiveTo = DateTime.Now;
        //        _vatService.Update(data);
        //        vat.VatId = 0;
        //        vat.EfectiveFrom = DateTime.Now;
        //        vat.CreatedBy = _session.UserId;
        //        vat.CreatedTime = DateTime.Now;
        //        vat.IsActive = true;

        //        _vatService.Insert(vat);
        //        await UnitOfWork.SaveChangesAsync();
        //        var prodData = _prodService.Queryable().Where(c => c.VatId == id).AsQueryable();
        //        var log = new ProductLog();
        //        foreach (var item in prodData)
        //        {
        //            log.VatId = item.VatId;
        //            log.Code = item.Code;
        //            log.Name = item.Name;
        //            log.CreatedBy = item.CreatedBy;
        //            log.EfectiveFrom = item.EfectiveFrom;
        //            log.EfectiveTo = item.EfectiveTo;
        //            log.ProductId = item.ProductId;
        //            _logService.Insert(log);

        //        }
        //        await UnitOfWork.SaveChangesAsync();
        //        foreach (var item in prodData)
        //        {
        //            item.VatId = vat.VatId;
        //            _prodService.Update(item);

        //        }
        //        await UnitOfWork.SaveChangesAsync();
        //        TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.SUCCESS_CLASSNAME;
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.ERROR_CLASSNAME;
        //        return View(vat);
        //    }
        //}









        //public async Task<IActionResult> Delete(int id)
        //{

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    try
        //    {
        //        var data = await _vatService.Query().SingleOrDefaultAsync(m => m.VatId == id, CancellationToken.None);
        //        data.IsActive = false;
        //        data.EfectiveTo = DateTime.Now;
        //        _vatService.Update(data);

        //        await UnitOfWork.SaveChangesAsync();

        //        TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.DELETE_CLASSNAME;
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.ERROR_CLASSNAME;
        //        return RedirectToAction(nameof(Index));
        //    }
        //}


    }
}