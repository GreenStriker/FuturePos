using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using URF.Core.Abstractions;
using vms.api.Utility;
using vms.entity.models;
using vms.entity.viewModels;

using vms.service.dbo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vms.api.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/sale")]
    [ApiController]
    public class SaleController : vms.api.Controllers.ControllerBase
    {

        private readonly ISaleService _service;
        private readonly IHostingEnvironment _environment;
        private readonly IUnitOfWork _unityOfWork;

        public SaleController(ISaleService service, IHostingEnvironment environment, IUnitOfWork unityOfWork) : base(environment, unityOfWork)
        {
            this._service = service;
            ClassName = "SaleController";
            this._environment = environment;
            this._unityOfWork = unityOfWork;


        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "hi", "reza" };
        }
        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetSaleById")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var email = await GetEmailFromClaim();
                var response = await _service.Query().SingleOrDefaultAsync(x => x.SalesId == id, CancellationToken.None);
                if (response != null)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]vmSale vm)
        {
            var createdBy = 7;//Convert.ToInt32(await GetUserIdFromClaim());
            var organizationId =5;//Convert.ToInt32(await GetCompanyIdFromClaim());

            try
            {
                if (vm.ContentInfoJson!=null)
                {
                    Content content;
                    foreach (var contentInfo in vm.ContentInfoJson)
                    {
                        content = new Content();
                        vm.ContentInfoJsonTest = new List<Content>();
                        var File = contentInfo.UploadFile;
                        var FileSaveFeedbackDto = await FileSaveAsync(File);
                        content.FileUrl = FileSaveFeedbackDto.FileUrl;
                        content.MimeType = FileSaveFeedbackDto.MimeType;
                        content.DocumentTypeId = contentInfo.DocumentTypeId;
                        vm.ContentInfoJsonTest.Add(content);
                    }
                }
                if (vm.SalesDetailList.Count > 0)
                {
                    vm.CreatedBy = createdBy;
                    vm.OrganizationId = organizationId;
                    vmSaleOrder sale = new vmSaleOrder();
                    sale.InvoiceNo = vm.InvoiceNo;
                    sale.VatChallanNo = vm.VatChallanNo;
                    sale.OrganizationId = vm.OrganizationId;
                    sale.DiscountOnTotalPrice = vm.DiscountOnTotalPrice;
                    sale.IsVatDeductedInSource = vm.IsVatDeductedInSource;
                    sale.CustomerId = vm.CustomerId;
                    sale.ReceiverName = vm.ReceiverName;
                    sale.ReceiverContactNo = vm.ReceiverContactNo;
                    sale.ShippingAddress = vm.ShippingAddress;
                    sale.ShippingCountryId = vm.ShippingCountryId;
                    sale.SalesTypeId = 1;
                    sale.SalesDeliveryTypeId = vm.SalesDeliveryTypeId;
                    sale.WorkOrderNo = vm.WorkOrderNo;
                    sale.SalesDate = DateTime.Now;
                    sale.ExpectedDeliveryDate = vm.ExpectedDeliveryDate;
                    sale.DeliveryDate = vm.DeliveryDate;
                    sale.DeliveryMethodId = vm.DeliveryMethodId;
                    sale.ExportTypeId = vm.ExportTypeId;
                    sale.LcNo = vm.LcNo;
                    sale.LcDate = vm.LcDate;
                    sale.BillOfEntry = vm.BillOfEntry;
                    sale.BillOfEntryDate = vm.BillOfEntryDate;
                    sale.DueDate = vm.DueDate;
                    sale.TermsOfLc = vm.TermsOfLc;
                    sale.CustomerPoNumber = vm.CustomerPoNumber;
                    sale.IsComplete = true;
                    sale.IsTaxInvoicePrined = false;
                    sale.CreatedBy = vm.CreatedBy;
                    sale.CreatedTime = DateTime.Now;
                    sale.SalesDetailList = vm.SalesDetailList;
                    sale.SalesPaymentReceiveJson = vm.SalesPaymentReceiveJson;
                    sale.ContentInfoJson = vm.ContentInfoJsonTest;
                    await _service.InsertData(sale);
                  
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("{id}", Name = "CreditNote")]
        public async Task<IActionResult> CreditNote([FromBody]vmCreditNote vm)
        {
            var createdBy = Convert.ToInt32(await GetUserIdFromClaim());
            var organizationId = Convert.ToInt32(await GetCompanyIdFromClaim());
            bool status = false;
            try
            {
                if (vm.CreditNoteDetails.Count > 0)
                {
                    vm.CreatedBy = createdBy;
                    vm.CreatedTime = DateTime.Now;

                    status = await _service.InsertCreditNote(vm);
                }

                if (status == true)
                {
                    return Ok();
                }
                else
                    return StatusCode(StatusCodes.Status204NoContent);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
