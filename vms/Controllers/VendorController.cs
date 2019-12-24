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
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _service;
   
        private readonly IConfiguration _configuration;
  
        public VendorController(
            ControllerBaseParamModel controllerBaseParamModel,
            IVendorService service
          
            ) : base(controllerBaseParamModel)
        {
           
            _service = service;
            _configuration = Configuration;
         
        }



        public async Task<IActionResult> Index(int? page, string search = null)
        {
            var data = await _service.Query().SelectAsync();
            string txt = search;
            //
            if (search != null)
            {
                search = search.ToLower().Trim();
                data = data.Where(c => c.Name.ToLower().Contains(search) || c.Address.ToString().Contains(search)  || c.ContactNo.ToString().Contains(search));

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


        public async Task<IActionResult> Create(Vendor Cus)
        {
            if (Cus.ContactNo.Any())
            {


                var data = new Vendor();
                 data = await   _service.GetByMobile(Cus.ContactNo);

                if (data == null) { 
                Cus.CreatedBy = _session.UserId;
                Cus.CreatedTime = DateTime.Now;
                //Cus.BranchId = _session.BranchId;

                //Cus.IsActive = true;


                _service.Insert(Cus);
                await UnitOfWork.SaveChangesAsync();
                TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.SUCCESS_CLASSNAME;


                return RedirectToAction(nameof(Index));

                }

                else
                {
                    TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.ERROR_CLASSNAME;
                    return View(Cus);

                }

            }
            TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.ERROR_CLASSNAME;
            return View(Cus);
        }




        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var User = await _service.Query()
        //        .SingleOrDefaultAsync(m => m.BranchId == id, CancellationToken.None);
        //    if (User == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(User);
        //}


        //[HttpPost]

        //public async Task<IActionResult> Edit(Branch bra)
        //{

        //    if (bra.BranchId == 0)
        //    {
        //        return NotFound();
        //    }

        //    try
        //    {
        //        var id = bra.BranchId;
        //        var data = await _service.Query().SingleOrDefaultAsync(m => m.BranchId == id, CancellationToken.None);
        //        //data.IsActive = false;
        //        //data.DeactiveDate = DateTime.Now;
        //        //_service.Update(data);
        //       // data.BranchId=id;
        //        data.Name = bra.Name;
        //        data.Mobile = bra.Mobile;
        //        data.Code = bra.Code;
        //       // data.Mobile = bra.Mobile;
        //        data.Address = bra.Address;
        //       // data.BranchHeadId = bra.BranchHeadId;
        //       // data.OpeningDate = bra.OpeningDate;
        //        //data.DeactiveDate = bra.DeactiveDate;
        //       // data.IsActive = bra.IsActive;
        //        data.CreatedBy = _session.UserId;
        //        data.CreatedTime = DateTime.Now;




        //        //bra.CreatedBy = _session.UserId;
        //        //bra.CreatedTime = DateTime.Now;
        //        //bra.OpeningDate = DateTime.Now;
        //        //bra.IsActive = true;
        //        _service.Update(data);
        //        await UnitOfWork.SaveChangesAsync();

        //        TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.SUCCESS_CLASSNAME;
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.ERROR_CLASSNAME;
        //        return View(bra);
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
        //        var data = await _service.Query().SingleOrDefaultAsync(m => m.BranchId == id, CancellationToken.None);
        //        data.IsActive = false;
        //        data.DeactiveDate = DateTime.Now;
        //        _service.Update(data);

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