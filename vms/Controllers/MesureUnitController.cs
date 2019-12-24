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
    public class MesureUnitController : ControllerBase
    {
        private readonly IMesureUnitService _service;
       
        
        private readonly IConfiguration _configuration;
      
       
        public MesureUnitController(
            ControllerBaseParamModel controllerBaseParamModel,
            IMesureUnitService service
          
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
                data = data.Where(c => c.Name.ToLower().Contains(search));

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


        public async Task<IActionResult> Create(MeasureUnit bra)
        {
            if (bra.Name.Any())
            {
                bra.CreatedBy = _session.UserId;
                bra.CreatedTime = DateTime.Now;
                bra.EffectiveFrom = DateTime.Now;
                bra.IsActive = true;


                _service.Insert(bra);
                await UnitOfWork.SaveChangesAsync();
                TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.SUCCESS_CLASSNAME;
                return RedirectToAction(nameof(Index));
            }
            TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.ERROR_CLASSNAME;
            return View(bra);
        }




        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var User = await _service.Query()
                .SingleOrDefaultAsync(m => m.MunitId == id, CancellationToken.None);
            if (User == null)
            {
                return NotFound();
            }
            return View(User);
        }


        [HttpPost]

        public async Task<IActionResult> Edit(MeasureUnit Maja)
        {

            if (Maja.MunitId == 0)
            {
                return NotFound();
            }

            try
            {
                var id = Maja.MunitId;
                var data = await _service.Query().SingleOrDefaultAsync(m => m.MunitId == id, CancellationToken.None);
                data.Name = Maja.Name;
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
                return View(Maja);
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
                var data = await _service.Query().SingleOrDefaultAsync(m => m.MunitId == id, CancellationToken.None);
                data.IsActive = false;
                data.EffectiveTo = DateTime.Now;
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