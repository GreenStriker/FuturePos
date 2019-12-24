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
    public class PayrollController : ControllerBase
    {


        private readonly ISettingService _setservice;
        private readonly IPayrollService _service;
        private readonly IPayrollDetailService _Detailservice;

        private readonly IEmployeService _empservice;
        private readonly IAtendenceService _attendservice;
        private readonly IAdvancedSalaryService _Advancedservice;
        private readonly IOvertimeService _OverTimeservice;
        private readonly IIncentiveService _Incentiveservice;
        private readonly IExpenceService _expenceeservice;
        private readonly ISalaryService _salaryeservice;


        private readonly IConfiguration _configuration;
      
        private readonly IProductLogService _logService;
        public PayrollController(
            ControllerBaseParamModel controllerBaseParamModel,
            IPayrollService service,
            IPayrollDetailService Detailservice,
            ISalaryService salaryeservice,
            ISettingService setservice,
            IExpenceService expenceeservice,


        IEmployeService empservice,
        IAtendenceService       attendservice,
        IAdvancedSalaryService  Advancedservice,
        IOvertimeService        OverTimeservice,
        IIncentiveService       Incentiveservice




       
            ) : base(controllerBaseParamModel)
        {
     
            _service = service;
            _Detailservice = Detailservice;
            _configuration = Configuration;
            _empservice = empservice;
              _setservice = setservice;
            _expenceeservice = expenceeservice;
            _salaryeservice = salaryeservice;
             _attendservice      = attendservice;
             _Advancedservice    = Advancedservice;
             _OverTimeservice     = OverTimeservice;
            _Incentiveservice = Incentiveservice;


        }



        public async Task<IActionResult> Index(int? page, string search = null)
        {

            var set = await _setservice.Query().SingleOrDefaultAsync(m => m.IsActive == true, CancellationToken.None);


            var data = await _service.Query().Include(c=>c.Branch).Include(c => c.Settings).Include(c => c.Expence.Payment).Include(c => c.Expence).Where(x=> x.BranchId == _session.BranchId).OrderByDescending(c => c.PayrollId).SelectAsync();
            string txt = search;

            if (search != null)
            {
                search = search.ToLower().Trim();
                data = data.Where(c => c.Year.ToString().ToLower().Contains(search));

            }
            if (txt != null)
            {
                ViewData[ViewStaticData.SEARCH_TEXT] = txt;
            }
            else
            {
                ViewData[ViewStaticData.SEARCH_TEXT] = string.Empty;

            }

            ViewBag.Active = set.IsCanGiveSalary;
            var pageNumber = page ?? 1;
            var listOfdata = data.ToPagedList(pageNumber, 10);
            return View(listOfdata);

        }





        public async Task<IActionResult> Create()
        {

            var set = await _setservice.Query().SingleOrDefaultAsync(m => m.IsActive == true, CancellationToken.None);

            var curentDate = DateTime.Now.AddMonths(-1);
          


            int month = Convert.ToInt32(curentDate.Month);
            int year = Convert.ToInt32(curentDate.Year);


            var data = await _service.Query().SingleOrDefaultAsync(m => m.Month == month && m.Year == year && m.BranchId == _session.BranchId, CancellationToken.None);


            if (data == null && set.IsCanGiveSalary==true)
            {

                var payroll = new Payroll();
                payroll.Month = month;
                payroll.Year = year;
                payroll.SettingsId = set.SettingsId;
                payroll.BranchId = _session.BranchId;
                payroll.CreatedBy = _session.UserId;
                payroll.CreatedTime = DateTime.Now;


                _service.Insert(payroll);

                await UnitOfWork.SaveChangesAsync();

                var employes = await _empservice.Query().Where(x => x.BranchId == _session.BranchId && x.IsActive==true).SelectAsync();
                decimal count = 0;

                foreach(var item in employes)
                {

                    decimal salaryOFtheMonth = 0;
                    if(set.IsattendenceCount==true)
                    {




                    }
            

                  var advancedSalary = _Advancedservice.Queryable().Where (x => x.EmloyId == item.EmployeId && x.IsActive == true).AsQueryable();
                    // var advancedSalary = await _Advancedservice.Query().Where(x => x.EmloyId == item.EmployeId && x.IsActive == true).SelectAsync();
                    // var advancedSalary = await _Advancedservice.Query().SingleOrDefaultAsync(m => m.EmloyId == item.EmployeId && m.IsActive == true, CancellationToken.None);
                    var Overtime =  _OverTimeservice.Queryable().Where(x => x.EmployId == item.EmployeId && x.IsActive == true).AsQueryable();
                    var Incentive =  _Incentiveservice.Queryable().Where(x => x.EmployId == item.EmployeId && x.IsActive == true).AsQueryable();
                    var salryBase = await _salaryeservice.Query().SingleOrDefaultAsync(m => m.EmployeId == item.EmployeId && m.IsActive == true, CancellationToken.None);
                    

                    decimal eadvance = 0, eover = 0, eincentive = 0;

                    if (advancedSalary != null)
                    {

                        foreach (var itms in advancedSalary)
                        {
                            eadvance = eadvance + itms.Amount;


                            itms.IsActive = false;
                            itms.Remarks = itms.Remarks + "Colosed in payroll " + payroll.PayrollId.ToString();
                            itms.PayrollId = payroll.PayrollId;
                            _Advancedservice.Update(itms);
                            //await UnitOfWork.SaveChangesAsync();


                        }



                    }
                    if (Overtime != null)
                    {

                        foreach (var itms in Overtime)
                        {
                            eover = eover + itms.OverTimeHoure;


                            itms.IsActive = false;
                            itms.Remarks = itms.Remarks + "Colosed in payroll " + payroll.PayrollId.ToString();
                            itms.PayrollId = payroll.PayrollId;
                            _OverTimeservice.Update(itms);
                            //await UnitOfWork.SaveChangesAsync();

                        }

                        eover *= set.OverTimeRatio;

                    }

                    if (Incentive != null)
                    {

                        foreach (var itms in Incentive)
                        {
                            eincentive = eincentive + itms.IncentivePoint;


                            itms.IsActive = false;
                            // itms.Remarks = itms.Remarks + "Colosed in payroll " + payroll.PayrollId.ToString();
                            itms.PayrollId = payroll.PayrollId;
                            _Incentiveservice.Update(itms);

                            //await UnitOfWork.SaveChangesAsync();

                        }



                    }

                    if (salryBase != null) { 


                    salaryOFtheMonth = salryBase.BaseSalary - eadvance +eover+eincentive;

                        var paydetail = new PayrollDetail();

                        paydetail.AdvancedAmount = eadvance;
                        paydetail.OverTimeAmount = eover;
                        paydetail.IncentiveAmount = eincentive;
                        paydetail.PayableSalary = salaryOFtheMonth;
                        paydetail.SalaryId = salryBase.SalaryId;
                        paydetail.PayrollId = payroll.PayrollId;
                        paydetail.BaseSalary = salryBase.BaseSalary;
                        paydetail.EmployeId = item.EmployeId;
                        _Detailservice.Insert(paydetail);


                        count = count + salaryOFtheMonth;


                    }


                    //await UnitOfWork.SaveChangesAsync();
                }


               // await UnitOfWork.SaveChangesAsync();

                //

                var expence = new Expence();

                expence.BranchId = _session.BranchId;
                expence.CreatedBy = _session.UserId;
                expence.ExpenceTypeId = 1;
                expence.ExpenceAmount = count;
                expence.ExpencePerson = _session.UserId;
                expence.ExpencePurpose = "Monthly Salary :"+month.ToString()+"-"+year.ToString();
                expence.IsActive = true;
                expence.CreatedTime = DateTime.Now;
                expence.ExpenceDate = DateTime.Now; 
                


                _expenceeservice.Insert(expence);

               // await UnitOfWork.SaveChangesAsync();




                var lastpayroll = await _service.Query().SingleOrDefaultAsync(m => m.PayrollId == payroll.PayrollId, CancellationToken.None);

                lastpayroll.ExpenceId = expence.ExpenceId;

                _service.Update(lastpayroll);

                await UnitOfWork.SaveChangesAsync();
                TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.SUCCESS_CLASSNAME;
                return RedirectToAction(nameof(Index));

            }



            TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.ERROR_CLASSNAME;
            return RedirectToAction(nameof(Index));


        }











        public async Task<IActionResult> PayrollDetails(int id , int? page, string search = null)
        {

           // var set = await _setservice.Query().SingleOrDefaultAsync(m => m.IsActive == true, CancellationToken.None);


            var data = await _Detailservice.Query().Include(c => c.Employe).Include(c => c.Salary).Where(x => x.PayrollId == id).SelectAsync();
            string txt = search;

            if (search != null)
            {
                search = search.ToLower().Trim();
                data = data.Where(c => c.Employe.Name.ToString().ToLower().Contains(search) || c.Employe.BanckAccountNo.ToString().ToLower().Contains(search));

            }
            if (txt != null)
            {
                ViewData[ViewStaticData.SEARCH_TEXT] = txt;
            }
            else
            {
                ViewData[ViewStaticData.SEARCH_TEXT] = string.Empty;

            }

            //ViewBag.Active = set.IsCanGiveSalary;
            var pageNumber = page ?? 1;
            var listOfdata = data.ToPagedList(pageNumber, 10);
            return View(listOfdata);

        }








        public async Task<IActionResult> AdvancedDetails(int id)
        {

            // var set = await _setservice.Query().SingleOrDefaultAsync(m => m.IsActive == true, CancellationToken.None);

            var details = await _Detailservice.Query().SingleOrDefaultAsync(m => m.PayrollDetailsId == id, CancellationToken.None);


            var data = await _Advancedservice.Query().Include(c => c.Emloy).Where(x => x.PayrollId == details.PayrollId && x.EmloyId == details.EmployeId).SelectAsync();
           
            return View(data);

        }


        public async Task<IActionResult> OverDetails(int id)
        {

            var details = await _Detailservice.Query().SingleOrDefaultAsync(m => m.PayrollDetailsId == id, CancellationToken.None);


            var data = await _OverTimeservice.Query().Include(c => c.Employ).Include(c=>c.Payroll.Settings).Where(x => x.PayrollId == details.PayrollId && x.EmployId == details.EmployeId).SelectAsync();

            return View(data);

        }




        public async Task<IActionResult> IncenDetails(int id)
        {

            var details = await _Detailservice.Query().SingleOrDefaultAsync(m => m.PayrollDetailsId == id, CancellationToken.None);


            var data = await _Incentiveservice.Query().Include(c => c.Employ).Where(x => x.PayrollId == details.PayrollId && x.EmployId == details.EmployeId).SelectAsync();

            return View(data);

        }




    }
}