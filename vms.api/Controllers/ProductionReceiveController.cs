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
    [Route("api/productionreceive")]
    [ApiController]
    public class ProductionReceiveController : ControllerBase
    {
        private readonly IProductionService _service;
        public ProductionReceiveController(IProductionService service, IHostingEnvironment environment, IUnitOfWork unityOfWork) : base(environment, unityOfWork)
        {
            this._service = service;
            ClassName = "ProductionReceiveController";
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var response = await _service.Query().SingleOrDefaultAsync(x => x.ProductionId == id, CancellationToken.None);
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Utility.vmProductionReceive vmProduction)
        {
            var createdBy = 7;// Convert.ToInt32(await GetUserIdFromClaim());
            var organizationId = 5;// Convert.ToInt32(await GetCompanyIdFromClaim());
            bool status = false;

            try
            {
                if (vmProduction.ContentInfoJson!=null)
                {
                    Content content;
                    foreach (var contentInfo in vmProduction.ContentInfoJson)
                    {
                        content = new Content();
                        vmProduction.ContentInfoJsonTest = new List<Content>();
                        var File = contentInfo.UploadFile;
                        var FileSaveFeedbackDto = await FileSaveAsync(File);
                        content.FileUrl = FileSaveFeedbackDto.FileUrl;
                        content.MimeType = FileSaveFeedbackDto.MimeType;
                        content.DocumentTypeId = contentInfo.DocumentTypeId;
                        vmProduction.ContentInfoJsonTest.Add(content);
                    }
                }
                if (vmProduction.ProductionReceiveDetailList.Count>0)
                {
                    vmProduction.CreatedBy = createdBy;
                    vmProduction.OrganizationId = organizationId;
                    entity.viewModels.vmProductionReceive productionReceive = new entity.viewModels.vmProductionReceive();
                    productionReceive.BatchNo = vmProduction.BatchNo;
                    productionReceive.OrganizationId = vmProduction.OrganizationId;
                    productionReceive.ProductionId = 1;
                    productionReceive.ProductId = vmProduction.ProductId;
                    productionReceive.ReceiveQuantity = vmProduction.ReceiveQuantity;
                    productionReceive.MeasurementUnitId = vmProduction.MeasurementUnitId;
                    productionReceive.ReceiveTime = vmProduction.ReceiveTime;
                    productionReceive.CreatedBy = createdBy;
                    productionReceive.CreatedTime = DateTime.Now;

                    productionReceive.ProductionReceiveDetailList = vmProduction.ProductionReceiveDetailList;
                    productionReceive.ContentInfoJson = vmProduction.ContentInfoJsonTest;
                    status = await _service.InsertData(productionReceive);
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