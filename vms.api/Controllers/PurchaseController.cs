using System;
using System.Collections.Generic;
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

namespace vms.api.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/Purchase")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseOrderService _service;
        private readonly IHostingEnvironment _environment;
        private readonly IUnitOfWork _unityOfWork;
        public PurchaseController(IPurchaseOrderService service, IHostingEnvironment environment, IUnitOfWork unityOfWork) : base(environment, unityOfWork)
        {
            this._service = service;
            ClassName = "PurchaseController";
            this._environment = environment;
            this._unityOfWork = unityOfWork;


        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var response = await _service.Query().SingleOrDefaultAsync(x => x.PurchaseId == id, CancellationToken.None);
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
        public async Task<IActionResult> Post([FromBody]vmPurchase vm)
        {
            var createdBy = 7;//Convert.ToInt32(await GetUserIdFromClaim());
            var organizationId = 5;// Convert.ToInt32(await GetCompanyIdFromClaim());
            bool status = false;
            try
            {
                //if (vm.ContentInfoJson.Any())
                //{
                //    Content content;
                //    foreach (var contentInfo in vm.ContentInfoJson)
                //    {
                //        content = new Content();
                //        vm.ContentInfoJsonTest = new List<Content>();
                //        var File = contentInfo.UploadFile;
                //        var FileSaveFeedbackDto = await FileSaveAsync(File);
                //        content.FileUrl = FileSaveFeedbackDto.FileUrl;
                //        content.MimeType = FileSaveFeedbackDto.MimeType;
                //        content.DocumentTypeId = contentInfo.DocumentTypeId;
                //        vm.ContentInfoJsonTest.Add(content);
                //    }
                //}

                if (vm.PurchaseOrderDetailList.Count > 0)
                {
                    VmPurchase vmPurchase = new VmPurchase();
                    vmPurchase.PurchaseOrderDetailList = vm.PurchaseOrderDetailList;
                    vmPurchase.ContentInfoJson = vm.ContentInfoJsonTest;
                    vmPurchase.PurchasePaymenJson = vm.PurchasePaymenJson;
                    vmPurchase.OrganizationId = organizationId;
                    vmPurchase.VendorId = vm.VendorId;
                    vmPurchase.VatChallanNo = vm.VatChallanNo;
                    vmPurchase.VatChallanIssueDate = vm.VatChallanIssueDate;
                    vmPurchase.VendorInvoiceNo = vm.VendorInvoiceNo;
                    vmPurchase.InvoiceNo = vm.InvoiceNo;
                    vmPurchase.PurchaseTypeId = vm.PurchaseTypeId;
                    vmPurchase.PurchaseReasonId = vm.PurchaseReasonId;
                    vmPurchase.DiscountOnTotalPrice = vm.DiscountOnTotalPrice;
                    vmPurchase.IsVatDeductedInSource = vm.IsVatDeductedInSource;
                    vmPurchase.PaidAmount = vm.PaidAmount;
                    vmPurchase.ExpectedDeliveryDate = vm.ExpectedDeliveryDate;
                    vmPurchase.DeliveryDate = DateTime.Now;
                    vmPurchase.LcNo = vm.LcNo;
                    vmPurchase.LcDate = vm.LcDate;
                    vmPurchase.BillOfEntry = vm.BillOfEntry;
                    vmPurchase.BillOfEntryDate = vm.BillOfEntryDate;
                    vmPurchase.DueDate = vm.DueDate;
                    vmPurchase.TermsOfLc = vm.TermsOfLc;
                    vmPurchase.PoNumber = vm.PoNumber;
                    vmPurchase.MushakGenerationId = vm.MushakGenerationId;
                    vmPurchase.IsComplete = true;
                    vmPurchase.CreatedBy = createdBy;
                    vmPurchase.CreatedTime = DateTime.Now;
                    vmPurchase.Flag = 1;
                    status = await _service.InsertData(vmPurchase);
                }

                if (status==true)
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


        [HttpPost("{id}", Name = "DebitNote")]
        public async Task<IActionResult> DebitNote([FromBody]vmDebitNote vm)
        {
            var createdBy = Convert.ToInt32(await GetUserIdFromClaim());
            var organizationId = Convert.ToInt32(await GetCompanyIdFromClaim());
            bool status = false;
            try
            {
                if (vm.DebitNoteDetails.Count > 0)
                {
                    vm.CreatedBy = createdBy;
                    vm.CreatedTime = DateTime.Now;

                    status = await _service.InsertDebitNote(vm);
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
    }
}