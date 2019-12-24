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
    public class ExpenceController : ControllerBase
    {
        private readonly IExpenceService _service;
        private readonly IExpenceTypeService _Typeservice;
        private readonly IEmployeService _Empservice;
        private readonly IPaymentService _payservice;
        private readonly IPaymentMethodService _paymethservice;
        private readonly IConfiguration _configuration;
    
        public ExpenceController(
            ControllerBaseParamModel controllerBaseParamModel,
            IExpenceService service,
            IExpenceTypeService Typeservice,
             IPaymentService payservice,
         IPaymentMethodService paymethservice,
        IEmployeService Empservice


            ) : base(controllerBaseParamModel)
        {
            
            _service = service;
            _Empservice = Empservice;
            _Typeservice = Typeservice;
            _configuration = Configuration;
            _payservice = payservice;
            _paymethservice = paymethservice;
        }



        public async Task<IActionResult> Index(int? page, string search = null)
        {
            var data = await _service.Query().Include(x => x.ExpenceType).Include(x=>x.ExpencePersonNavigation).Include(x=>x.Branch).Include(x=>x.Payment).Where(x => x.IsActive == true).OrderByDescending(c=>c.ExpenceId).SelectAsync();
            string txt = search;
            
            if (search != null)
            {
                search = search.ToLower().Trim();
                data = data.Where(c => c.ExpenceType.Name.ToLower().Contains(search) || c.ExpencePersonNavigation.Name.ToString().Contains(search) || c.ExpenceId.ToString().Contains(search) || c.Branch.Name.ToString().Contains(search) || c.ExpenceAmount.ToString().Contains(search) || c.ExpenceDate.ToString().Contains(search) || c.ExpencePurpose.ToString().Contains(search));

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

            var Employe = await _Empservice.GetAll();
            var ExType = await _Typeservice.GetAll();
            var paytype = await _paymethservice.GetAll();
            IEnumerable<SelectListItems> Employes = Employe.Where(c => c.IsActive == true && c.BranchId == _session.BranchId).Select(s => new SelectListItems
            {
                Id = s.EmployeId,
                Name = s.Name
            });
            IEnumerable<SelectListItems> ExTypes = ExType.Where(c => c.IsActive == true).Select(s => new SelectListItems
            {
                Id = s.ExpenceTypeId,
                Name = s.Name
            });
            IEnumerable<SelectListItems> PayTypes = paytype.Select(s => new SelectListItems
            {
                Id = s.PaymentMethodId,
                Name = s.Name
            });

            var expence = new VmExpence()
            {

                Employes = Employes,
                Types = ExTypes,
                pType = PayTypes

                

            };



            return View(expence);
        }

        [HttpPost]


        public async Task<IActionResult> Create(VmExpence Ep)
        {
            if (Ep.ExpenceAmount!=null)
            {

                if (Ep.Payment.AmountPaid != null)
                {
                    var payment = new Payment();

                    payment.AmountPaid = Ep.Payment.AmountPaid;
                    payment.CreatedBy = -_session.UserId;
                    payment.Createdtime = DateTime.Now;
                    payment.PaymentMethodId = Ep.Payment.PaymentMethodId;
                    payment.TransactionId = Ep.Payment.TransactionId;
                    payment.Remark = Ep.Payment.Remark;

                    _payservice.Insert(payment);
                    await UnitOfWork.SaveChangesAsync();




                    Ep.CreatedBy = _session.UserId;
                    Ep.CreatedTime = DateTime.Now;
                    

                    Ep.IsActive = true;

                    Ep.PaymentId = payment.PaymetId;

                    Ep.BranchId = _session.BranchId;
                    _service.Insert(Ep);
                    await UnitOfWork.SaveChangesAsync();


                }
                else
                {
                    Ep.CreatedBy = _session.UserId;
                    Ep.CreatedTime = DateTime.Now;


                    Ep.IsActive = true;

                 

                    Ep.BranchId = _session.BranchId;
                    _service.Insert(Ep);
                    await UnitOfWork.SaveChangesAsync();
                }




               
                TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.SUCCESS_CLASSNAME;
                return RedirectToAction(nameof(Index));
            }
            TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.ERROR_CLASSNAME;
            return View(Ep);
        }




        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var User = await _service.Query()
        //        .SingleOrDefaultAsync(m => m.ExpenceTypeId == id, CancellationToken.None);
        //    if (User == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(User);
        //}


        //[HttpPost]

        //public async Task<IActionResult> Edit(ExpenceType ep)
        //{

        //    if (ep.ExpenceTypeId == 0)
        //    {
        //        return NotFound();
        //    }

        //    try
        //    {
        //        var id = ep.ExpenceTypeId;
        //        var data = await _service.Query().SingleOrDefaultAsync(m => m.ExpenceTypeId == id, CancellationToken.None);
        //        //data.IsActive = false;
        //        //data.DeactiveDate = DateTime.Now;
        //        //_service.Update(data);
        //        // data.BranchId=id;
        //        data.Name = ep.Name;
        //        data.Remark = ep.Remark;

        //        // data.BranchHeadId = bra.BranchHeadId;
        //        // data.OpeningDate = bra.OpeningDate;
        //        //data.DeactiveDate = bra.DeactiveDate;
        //        // data.IsActive = bra.IsActive;
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
        //        return View(ep);
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
        //        var data = await _service.Query().SingleOrDefaultAsync(m => m.ExpenceTypeId == id, CancellationToken.None);
        //        data.IsActive = false;

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