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
    public class AttendenceController : ControllerBase
    {


        private readonly ISettingService _setservice;
        private readonly IAtendenceService _service;
        private readonly IAttendenceDetailService _DetailsService;
        private readonly IProductService _prodService;
        private readonly IEmployeService _Empservice;
        private readonly IConfiguration _configuration;
        //private readonly IOrganizationService _orgcConfiguration;
        private readonly IProductLogService _logService;
        public AttendenceController(
            ControllerBaseParamModel controllerBaseParamModel,
            ISettingService setservice,
            IAtendenceService service,
            IEmployeService Empservice,
        IAttendenceDetailService DetailsService,
            IProductService prodService,
            IProductLogService logService
            //IRightService rightService, 
            //IOrganizationService orgcConfiguration
            ) : base(controllerBaseParamModel)
        {
            _logService = logService;
            _service = service;
            _configuration = Configuration;
            _DetailsService = DetailsService;
            _prodService = prodService;
            //_rightService = rightService;
            _Empservice = Empservice;
            _setservice = setservice;
        }



        public async Task<IActionResult> Index(int? page, string search = null)
        {

            var data = await _service.Query().Where(x=> x.IsActive == true).OrderByDescending(c=>c.AtendenceId).SelectAsync();
            string txt = search;

            if (search != null)
            {
                search = search.ToLower().Trim();
                data = data.Where(c => c.Year.ToString().ToLower().Contains(search) || c.Branch.Name.ToString().Contains(search));

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





        public async Task<IActionResult> Create()
        {

            var curentDate = DateTime.Now.AddMonths(-1);


            int month = Convert.ToInt32(curentDate.Month);
            int year = Convert.ToInt32(curentDate.Year);


            var data =  await _service.Query().SingleOrDefaultAsync(m => m.Month == month && m.Year==year && m.IsActive == true && m.BranchId == _session.BranchId, CancellationToken.None);


            if (data==null)
            {
                var Employe = await _Empservice.GetAll();

                IEnumerable<SelectListItems> Employes = Employe.Where(c => c.IsActive == true && c.BranchId == _session.BranchId).Select(s => new SelectListItems
                {
                    Id = s.EmployeId,
                    Name = s.Name,
                    Present = 0,
                });


                var Attendence = new Atendence()
                {

                    Employes = Employes



                };

                Attendence.Month = month;
                Attendence.Year = year;










                return View(Attendence);

            }



            TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.ERROR_CLASSNAME;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]


        public IActionResult Create(Atendence vat)
        {
            //    if (ModelState.IsValid)
            //    {
            //        vat.CreatedBy = _session.UserId;
            //        vat.CreatedTime = DateTime.Now;
            //        vat.EfectiveFrom = DateTime.Now;

            //        vat.IsActive = true;

            //        _vatService.Insert(vat);
            //        await UnitOfWork.SaveChangesAsync();
            //        TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.SUCCESS_CLASSNAME;
            //        return RedirectToAction(nameof(Index));
            //    }
            //    TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.ERROR_CLASSNAME;
            //    return View(vat);
            //}

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
            return View(vat);
        }


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