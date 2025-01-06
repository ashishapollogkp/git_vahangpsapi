using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vahangpsapi.Interfaces;
using vahangpsapi.Models.Manufacturer;
using vahangpsapi.Models.User;
using vahangpsapi.Services;

namespace vahangpsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributorController : ControllerBase
    {
        private readonly IDistributorService _employeeService;
        protected APIResponse _response;
        public DistributorController(IDistributorService employeeService)
        {
            _employeeService = employeeService;
            _response = new();
        }

        [HttpPost]
        [Route("AddDistributor")]
        public async Task<ActionResult<APIResponse>> AddDistributor([FromBody] ManufacturerAddDTO employee)
        {

            try
            {

                if (employee == null)
                {
                    return BadRequest(employee);
                }

                _response = await _employeeService.AddDistributor(employee);


            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;








        }

        // PUT api/<EmployeeController>/5
        [HttpPost]
        [Route("UpdateDistributor")]
        public async Task<ActionResult<APIResponse>> UpdateDistributor([FromBody] ManufacturerEditDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || updateDTO.EmpId == 0)
                {
                    return BadRequest();
                }

                _response = await _employeeService.UpdateDistributor(updateDTO);


            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpPost]
        [Route("DistributorList")]
        public async Task<ActionResult<APIResponse>> DistributorList(UserReq req)
        {
            try
            {

                _response = await _employeeService.DistributorList(req);

                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;


        }
    }
}
