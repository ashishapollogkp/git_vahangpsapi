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
    public class SalesManagerController : ControllerBase
    {
        private readonly ISalesManagerService _employeeService;
        protected APIResponse _response;
        public SalesManagerController(ISalesManagerService employeeService)
        {
            _employeeService = employeeService;
            _response = new();
        }



        [HttpPost]
        [Route("AddSalesManager")]
        public async Task<ActionResult<APIResponse>> AddSalesManager([FromBody] ManufacturerAddDTO employee)
        {

            try
            {

                if (employee == null)
                {
                    return BadRequest(employee);
                }

                _response = await _employeeService.AddSalesManager(employee);


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
        [Route("UpdateSalesManager")]
        public async Task<ActionResult<APIResponse>> UpdateSalesManager([FromBody] ManufacturerEditDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || updateDTO.EmpId == 0)
                {
                    return BadRequest();
                }

                _response = await _employeeService.UpdateSalesManager(updateDTO);


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
        [Route("GetSalesManagerList")]
        public async Task<ActionResult<APIResponse>> GetSalesManagerList(UserReq req)
        {
            try
            {

                _response = await _employeeService.GetSalesManagerList(req);

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
