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
using vms.entity.StoredProcedureModel;
using vms.entity.viewModels;
using vms.service.dbo;
using vms.service.dbo.StoredProdecure;
using vms.utility.StaticData;

namespace vms.api.Controllers
{
    [Produces(ControllerStaticData.RESPONSE_TYPE_JSON)]
    [Route("api/[controller]"), Authorize]
    [ApiController]
    public class DamageController : ControllerBase
    {
        private readonly IDamageTypeService _service;
        private readonly IDamageInvoiceListService _invoiceService;
        protected DamageController(IDamageInvoiceListService invoiceService,IDamageTypeService service,IHostingEnvironment hostingEnvironment, IUnitOfWork unitOfWork) : base(hostingEnvironment, unitOfWork)
        {
            this._service = service;
            this._invoiceService = invoiceService;
            ClassName = "DamageController";
        }

        // GET: api/Address
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                int clientId = Convert.ToInt32(await GetCompanyIdFromClaim());
                var response = await _invoiceService.GetDamageList(clientId);

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
        // GET: api/Address/id
        [HttpGet("{id}", Name = "GetDamageById")]
        public async Task<IActionResult> Get(int id)
        {
            int companyId = Convert.ToInt32(await GetCompanyIdFromClaim());
            try
            {
                var listOfDamange = await _invoiceService.GetDamageList(companyId);
                var  singleDamageInfo = listOfDamange.Where(c => c.DamageId == id).SingleOrDefault();
                if (singleDamageInfo != null)
                {
                    return Ok(singleDamageInfo);
                }
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]VmDamage model)
        {
            try
            {
                if (model.DamageQty > 0)
                {
                    var inmodel = new SpDamageInsert
                    {
                        OrganizationId = Convert.ToInt32(await GetCompanyIdFromClaim()),
                        ProductId = model.ProductId,
                        StockInId = model.StockInId,
                        DamageTypeId = model.DamageTypeId,
                        DamageQty = model.DamageQty,
                        CreatedBy = Convert.ToInt32(await GetUserIdFromClaim()),
                        Description = model.Description
                    };

                    await _invoiceService.InsertDamage(inmodel);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}