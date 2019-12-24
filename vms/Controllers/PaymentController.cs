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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;



        private readonly IConfiguration _configuration;

        public PaymentController(
            ControllerBaseParamModel controllerBaseParamModel,
            IPaymentService service


            ) : base(controllerBaseParamModel)
        {

            _service = service;

        }



        public async Task<IActionResult> Index(int? page, string search = null)
        {
            var data = await _service.Query().Include(x=> x.PaymentMethod).OrderByDescending(c => c.PaymetId).SelectAsync();
            string txt = search;

            if (search != null)
            {
                search = search.ToLower().Trim();
                data = data.Where(c => c.AmountPaid.ToString().ToLower().Contains(search) || c.TransactionId.ToLower().Contains(search) || c.PaymetId.ToString().ToLower().Contains(search) || c.PaymentMethod.Name.ToLower().Contains(search)|| c.Remark.ToLower().Contains(search));

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

    }
}