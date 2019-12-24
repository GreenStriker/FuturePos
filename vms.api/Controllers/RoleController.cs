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
using vms.entity.models;
using vms.service.dbo;
using vms.utility.StaticData;

namespace vms.api.Controllers
{
    [Produces(ControllerStaticData.RESPONSE_TYPE_JSON)]
    [Route("api/role"), Authorize]
    public class RoleController : vms.api.Controllers.ControllerBase
    {
        private readonly IRoleService _service;
        public RoleController(IRoleService service, IHostingEnvironment environment, IUnitOfWork unityOfWork) : base(environment, unityOfWork)
        {
            this._service = service;
            ClassName = "RoleController";
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
        [HttpGet("{id}", Name = "GetRoleById")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var response = await _service.Query().SingleOrDefaultAsync(x => x.RoleId == id, CancellationToken.None);
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
        public async Task<IActionResult> Post([FromBody]Role p_role)
        {
            try
            {
                int CompanyId = Convert.ToInt32(await GetCompanyIdFromClaim());
                int UserId = Convert.ToInt32(await GetUserIdFromClaim());
                _service.Insert(p_role);
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
        public async Task<IActionResult> Put([FromBody]Role p_address)
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
        public async Task<IActionResult> Delete(Role Address)
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