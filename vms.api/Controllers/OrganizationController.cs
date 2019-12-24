using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vms.entity.models;
using vms.service;
using vms.utility.StaticData;
using System;
using System.Threading;
using System.Threading.Tasks;
using URF.Core.Abstractions;
using vms.service.dbo;

namespace vms.Controllers
{
    [Produces(ControllerStaticData.RESPONSE_TYPE_JSON)]
    [Route("api/organization"), Authorize]
    public class OrganizationController : vms.api.Controllers.ControllerBase
    {
        private readonly IOrganizationService _service;
        public OrganizationController(IOrganizationService service, IHostingEnvironment environment, IUnitOfWork unityOfWork) : base(environment, unityOfWork)
        {
            this._service = service;
            ClassName = "OrganizationController";
        }
        // GET: api/Address
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                //int clientId = Convert.ToInt32(await GetCompanyIdFromClaim());
                var response = await _service.Query().SelectAsync();

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
        [HttpGet("{id}", Name = "GetOrganizationById")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var response = await _service.Query().SingleOrDefaultAsync(x => x.OrganizationId == id, CancellationToken.None);
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

        // POST: api/Address
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Organization p_organization)
        {
            try
            {
                int CompanyId = Convert.ToInt32(await GetCompanyIdFromClaim());
                int UserId = Convert.ToInt32(await GetUserIdFromClaim());
                _service.Insert(p_organization);
                await _unitOfWork.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        // PUT: api/Address/
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Organization p_address)
        {
            try
            {
                _service.Update(p_address);
                await _unitOfWork.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE: api/Address/
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Organization Address)
        {
            try
            {
                int CompanyId = Convert.ToInt32(await GetCompanyIdFromClaim());
                _service.Delete(Address);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
