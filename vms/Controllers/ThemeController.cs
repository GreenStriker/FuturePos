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
    public class ThemeController : ControllerBase
    {



        private readonly IThemeService _service;
        private readonly IColorService _Colservice;
        private readonly IConfiguration _configuration;

        public ThemeController(
            ControllerBaseParamModel controllerBaseParamModel,
            IThemeService service,
            IColorService Colservice
      
            ) : base(controllerBaseParamModel)
        {

            _service = service;
            _Colservice=Colservice;
            _configuration = Configuration;
       
        }



        public async Task<IActionResult> Index()
        {
           var data = await _service.Query().SingleOrDefaultAsync(m => m.Uid == _session.UserId, CancellationToken.None);
            var dataCol = await _Colservice.Query().SelectAsync();

          
          


            return View(dataCol);

        }





        //public IActionResult Create()
        //{

        //    return View();
        //}

        //[HttpPost]


        //public async Task<IActionResult> Create(Vat vat)
        //{
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
        //    return View(exportType);
        //}


      

        public async Task<IActionResult> Edit(int id)
        {



            try
            {
                var data = await _service.Query().SingleOrDefaultAsync(m => m.Uid == _session.UserId, CancellationToken.None);


                var datacol = await _Colservice.Query().SingleOrDefaultAsync(m => m.ColorId == id, CancellationToken.None);



                if (data.ColorId == id)
                {
                    TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.ERROR_CLASSNAME;
                    return RedirectToAction(nameof(Index));

                }
                data.ColorId = id;
                _service.Update(data);
                await UnitOfWork.SaveChangesAsync();
                //  return RedirectToAction("Index", "Default");



                _session.FristThe = datacol.Frist;
                _session.secThe = datacol.Sec;
                _session.thirdThe = datacol.Third;
                _session.FroreThe = datacol.Forth;


                var ses = new vmSession();

                ses = _session;

                HttpContext.Session.SetComplexData(vms.utility.StaticData.ControllerStaticData.SESSION, ses);

                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.ERROR_CLASSNAME;
                return RedirectToAction(nameof(Index));
            }
        }

        

    }
}